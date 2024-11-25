using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InvestmentController : ControllerBase
{
    private const double TB = 1.08;  // Taxa do banco: 108%
    private const double CDI = 0.009;  // CDI de 0.9%

    [HttpPost("calculate")]
    public IActionResult CalculateInvestment([FromBody] InvestmentRequest request)
    {
        if (request.PrazoMeses < 1)
            return BadRequest("Prazo deve ser maior que 1 mês.");

        double valorFinalBruto = request.ValorInicial;

        for (int i = 0; i < request.PrazoMeses; i++)
        {
            valorFinalBruto *= (1 + (CDI * TB));
        }

        double imposto = GetImposto(request.PrazoMeses);
        double valorLiquido = valorFinalBruto * (1 - imposto);

        return Ok(new
        {
            ResultadoBruto = valorFinalBruto,
            ResultadoLiquido = valorLiquido
        });
    }

    private double GetImposto(int meses)
    {
        if (meses <= 6) return 0.225;  // 22.5% de imposto
        if (meses <= 12) return 0.20;  // 20% de imposto
        if (meses <= 24) return 0.175;  // 17.5% de imposto
        return 0.15;  // 15% de imposto
    }
}

public class InvestmentRequest
{
    public double ValorInicial { get; set; }
    public int PrazoMeses { get; set; }
}
