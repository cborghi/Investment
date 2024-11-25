using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace InvestimentApi.Tests
{
    public class InvestimentoControllerTests
    {
        [Fact]
        public void TestCalcularInvestimento()
        {
            var controller = new InvestmentController();
            var request = new InvestmentRequest { ValorInicial = 1000, PrazoMeses = 12 };

            var result = controller.CalculateInvestment(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = okResult.Value as dynamic;

            Assert.Equal(1200, value.Bruto);
            Assert.Equal(960, value.Liquido);
        }
    }
}
