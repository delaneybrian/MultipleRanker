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
                .SetOpponentParticipantId(Guid.NewGuid())
                .ApplyMatch(1, 2)
                .ApplyMatch(1, 2)
                .AssertSomething();

        public class TestContext
        {
            private ParticipantRatingModel _sut;

            private Guid _opponentId;

            public TestContext()
            {
                _sut = new ParticipantRatingModel(Guid.NewGuid(), 1);
            }

            public TestContext SetOpponentParticipantId(Guid opponentId)
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
