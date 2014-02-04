﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IrcClientStateTests.cs" company="Helpmebot Development Team">
//   Helpmebot is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Helpmebot is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with Helpmebot.  If not, see http://www.gnu.org/licenses/ .
// </copyright>
// <summary>
//   Defines the IrcClientStateTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Helpmebot.Tests.IRC
{
    using System.IO;
    using System.Linq;

    using Helpmebot.IRC;
    using Helpmebot.IRC.Interfaces;

    using Moq;

    using NUnit.Framework;

    using DataReceivedEventArgs = Helpmebot.IRC.Events.DataReceivedEventArgs;

    /// <summary>
    /// The IRC client state tests.
    /// </summary>
    [TestFixture]
    public class IrcClientStateTests : TestBase
    {
        /// <summary>
        /// The client.
        /// </summary>
        private IrcClient client;

        /// <summary>
        /// The network client.
        /// </summary>
        private Mock<INetworkClient> networkClient;

        /// <summary>
        /// The do setup.
        /// </summary>
        /// <param name="nickName">
        /// The nick Name.
        /// </param>
        public void DoSetup(string nickName)
        {
            this.networkClient = new Mock<INetworkClient>();
            this.client = new IrcClient(this.networkClient.Object, this.Logger.Object, this.ConfigurationHelper.Object, nickName, "user", "rn", "pw");
        }

        /// <summary>
        /// Tests read from WHO and my JOIN.
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest0()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test0.log", "stwalker|test");

            // assert
            this.networkClient.Verify(x => x.Send("WHO ##stwalkerster %uhnatfc,001"));
            this.networkClient.Verify(x => x.Send("WHO ##stwalkerster-development %uhnatfc,001"));

            var channels = this.client.Channels;
            Assert.That(channels.Count, Is.EqualTo(2));
            Assert.That(channels.ContainsKey("##stwalkerster-development"), Is.True);
            Assert.That(channels.ContainsKey("##stwalkerster"), Is.True);

            Assert.That(channels["##stwalkerster"].Users.Count, Is.EqualTo(3));
            Assert.That(channels["##stwalkerster-development"].Users.Count, Is.EqualTo(4));

            Assert.That(channels["##stwalkerster-development"].Users["ChanServ"].Operator, Is.True);
            Assert.That(channels["##stwalkerster-development"].Users["ChanServ"].User.Account, Is.Null);
            Assert.That(channels["##stwalkerster-development"].Users["Helpmebot"].Voice, Is.True);
            Assert.That(channels["##stwalkerster-development"].Users["Helpmebot"].User.Account, Is.EqualTo("helpmebot"));
        }

        /// <summary>
        /// The parser test for QUIT and MODE +o
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif        
        public void ParserTest1()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test1.log", "stwalker|test");

            // assert
            this.networkClient.Verify(x => x.Send("WHO ##stwalkerster %uhnatfc,001"));
            this.networkClient.Verify(x => x.Send("WHO ##stwalkerster-development %uhnatfc,001"));

            var channels = this.client.Channels;
            Assert.That(channels.Count, Is.EqualTo(2));
            Assert.That(channels.ContainsKey("##stwalkerster-development"), Is.True);
            Assert.That(channels.ContainsKey("##stwalkerster"), Is.True);

            var stw = channels["##stwalkerster"];
            var stwdev = channels["##stwalkerster-development"];

            Assert.That(stw.Users.Count, Is.EqualTo(2));
            Assert.That(stwdev.Users.Count, Is.EqualTo(3));

            Assert.That(stwdev.Users.ContainsKey("stwalkerster"), Is.False);
            Assert.That(stw.Users.ContainsKey("stwalkerster"), Is.False);

            Assert.That(stwdev.Users["ChanServ"].Operator, Is.True);
            Assert.That(stwdev.Users["ChanServ"].User.Account, Is.Null);
            Assert.That(stwdev.Users["Helpmebot"].Voice, Is.True);
            Assert.That(stwdev.Users["Helpmebot"].User.Account, Is.EqualTo("helpmebot"));
            Assert.That(stwdev.Users["stwalker|test"].Operator, Is.EqualTo(true));
            Assert.That(stwdev.Users["stwalker|test"].Voice, Is.EqualTo(true));
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest2()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test2.log", "stwalkerster___");
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest3()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test3.log", "stwalkerster___");
        }

        /// <summary>
        /// Just throwing data at this... - nick test
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest4()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test4.log", "stwalkerster___");

            Assert.That(this.client.Channels["#wikipedia-en"].Users.ContainsKey("FunPika_"), Is.False);
            Assert.That(this.client.Channels["#wikipedia-en"].Users.ContainsKey("FunPikachu"), Is.True);
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest5()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test5.log", "stwalkerster___");
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest6()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test6.log", "stwalkerster___");
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTest7()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test7.log", "stwalkerster___");
        }

        /// <summary>
        /// Just throwing data at this...
        /// </summary>
        [Test]
#if ! PARSERTESTS
        [Ignore("Parser tests disabled.")]
#endif
        public void ParserTestNickChange()
        {
            // run the test file from \\DORADO\store
            this.RunTestFile(@"A:\home\helpmebot\parsertests\test0.log", "stwalker|test");

            var channels = this.client.Channels;

            Assert.That(channels.ContainsKey("##stwalkerster-development"), Is.True);
            Assert.That(channels.ContainsKey("##stwalkerster"), Is.True);

            Assert.That(channels["##stwalkerster"].Users.Count, Is.EqualTo(3));
            Assert.That(channels["##stwalkerster-development"].Users.Count, Is.EqualTo(4));

            this.RaiseEvent(":Aranda56_!~chatzilla@c-98-242-146-227.hsd1.fl.comcast.net JOIN ##stwalkerster-development * :New Now Know How");
            this.RaiseEvent(":Aranda56_!~chatzilla@c-98-242-146-227.hsd1.fl.comcast.net JOIN ##stwalkerster * :New Now Know How");

            Assert.That(channels["##stwalkerster"].Users.Count, Is.EqualTo(4));
            Assert.That(channels["##stwalkerster-development"].Users.Count, Is.EqualTo(5));
            Assert.That(channels["##stwalkerster"].Users.ContainsKey("Aranda56_"), Is.True);
            Assert.That(channels["##stwalkerster-development"].Users.ContainsKey("Aranda56_"), Is.True);
            Assert.That(this.client.UserCache.ContainsKey("Aranda56_"), Is.True);

            this.RaiseEvent(":Aranda56_!~chatzilla@c-98-242-146-227.hsd1.fl.comcast.net NICK :Aranda56");

            Assert.That(channels["##stwalkerster"].Users.Count, Is.EqualTo(4));
            Assert.That(channels["##stwalkerster-development"].Users.Count, Is.EqualTo(5));
            Assert.That(channels["##stwalkerster"].Users.ContainsKey("Aranda56_"), Is.False);
            Assert.That(channels["##stwalkerster-development"].Users.ContainsKey("Aranda56_"), Is.False);

            Assert.That(channels["##stwalkerster"].Users.ContainsKey("Aranda56"), Is.True);
            Assert.That(channels["##stwalkerster-development"].Users.ContainsKey("Aranda56"), Is.True);

            Assert.That(this.client.UserCache.ContainsKey("Aranda56"), Is.True);
            Assert.That(this.client.UserCache.ContainsKey("Aranda56_"), Is.False);

            Assert.That(this.client.UserCache["Aranda56"].Account, Is.Null);
            Assert.That(channels["##stwalkerster"].Users["Aranda56"].User.Account, Is.Null);
            Assert.That(channels["##stwalkerster-development"].Users["Aranda56"].User.Account, Is.Null);

            this.RaiseEvent(":Aranda56!~chatzilla@c-98-242-146-227.hsd1.fl.comcast.net ACCOUNT Aranda56");
            
            Assert.That(this.client.UserCache["Aranda56"].Account, Is.EqualTo("Aranda56"));
            Assert.That(channels["##stwalkerster"].Users.ContainsKey("Aranda56"), Is.True);
            Assert.That(channels["##stwalkerster-development"].Users.ContainsKey("Aranda56"), Is.True);
            Assert.That(channels["##stwalkerster"].Users["Aranda56"].User.Account, Is.EqualTo("Aranda56"));
            Assert.That(channels["##stwalkerster-development"].Users["Aranda56"].User.Account, Is.EqualTo("Aranda56"));
        }

        /// <summary>
        /// The run test file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="nickname">
        /// The nickname.
        /// </param>
        private void RunTestFile(string fileName, string nickname)
        {
            this.DoSetup(nickname);

            var lines = File.ReadAllLines(fileName)
                .Where(x => x.StartsWith("> "))
                .Select(x => x.Substring(2));

            foreach (string line in lines)
            {
                this.RaiseEvent(line);
            }

            Assert.That(this.client.NickTrackingValid, Is.True);
        }

        /// <summary>
        /// The raise event.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        private void RaiseEvent(string line)
        {
            this.networkClient.Raise(x => x.DataReceived += null, new DataReceivedEventArgs(line));
        }
    }
}
