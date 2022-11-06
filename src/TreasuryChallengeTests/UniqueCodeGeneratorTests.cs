using System.Diagnostics;
using TreasuryChallenge;
namespace TreasuryChallengeTests
{
    public class UniqueCodeGeneratorTests
    {
        [Fact]
        public async Task GenerateCodes_Should_Generate_unique_codes()
        {
            //Arrange
            //Act
            var uniqueCodes = (await UniqueCodeGenerator.Generate(10000000)).ToString().Split("\n");

            //Assert
            Assert.Equal(uniqueCodes.Count(), uniqueCodes.Distinct().Count());

        }

        [Theory]
        [InlineData(10, 50)]
        [InlineData(10000, 100)]
        [InlineData(100000, 200)]
        [InlineData(1000000, 1500)]

        public async Task GenerateCodes_Should_generate_using_less_time(int quantity, int maxTime)
        {
            //Arrange
            //Act
            var time = Stopwatch.StartNew();
            await UniqueCodeGenerator.Generate(quantity);
            time.Stop();

            //Assert
            Assert.True(time.ElapsedMilliseconds < maxTime);

        }
    }
}