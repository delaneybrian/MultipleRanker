using System;
using MultipleRanker.Domain;
using NUnit.Framework;

namespace MultipleRanker.Tests.Unit
{
    [TestFixture]
    public class ParticipantModelTests
    {
        public TestContext _context;

        [SetUp]
        public void SetUp() => _context = new TestContext();

        [Test]
        public void SampleTest() =>
            _context
                .SetOpponentId(Guid.NewGuid())
                .ApplyMatch(1, 2)
                .ApplyMatch(1, 2)
                .AssertSomething();

        public class TestContext
        {
            private ParticipantRankingModel _sut;

            private Guid _opponentId;

            public TestContext()
            {
                _sut = new ParticipantRankingModel();
            }

            public TestContext SetOpponentId(Guid opponentId)
            {
                _opponentId = opponentId;

                return this;
            }

            public TestContext ApplyMatch(
                int score,
                int opponentScore)
            {
                _sut.AddResultVersus(_opponentId, score, opponentScore);

                return this;
            }

            public TestContext AssertSomething()
            {
                Assert.True(true);

                return this;
            }
        }
    }
}
