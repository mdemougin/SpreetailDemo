using Moq;
using SpreetailDemo.Implementations;
using SpreetailDemo.Services.Interfaces;
using SpreetailDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpreetailDemoTest
{
    public class ProgramTests
    {
        Mock<IDictionaryService> mockDictionaryService;

        public ProgramTests()
        {
            mockDictionaryService = new Mock<IDictionaryService>();
        }

        [Fact]
        public void TestKeysLowerHappyPath()
        {
            Program.ParseCommand("keys", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Keys());
        }

        [Fact]
        public void TestKeysUpperHappyPath()
        {
            Program.ParseCommand("KEYS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Keys());
        }

        [Fact]
        public void TestKeysExcessParameterFails()
        {
            Program.ParseCommand("KEYS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestMembersLowerHappyPath()
        {
            Program.ParseCommand("members key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Members("key"));
        }

        [Fact]
        public void TestMembersUpperHappyPath()
        {
            Program.ParseCommand("MEMBERS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Members("key"));
        }

        [Fact]
        public void TestMembersAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("MEMBERS \"key key\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Members("key key"));
        }

        [Fact]
        public void TestMembersExcessParametersFails()
        {
            Program.ParseCommand("MEMBERS key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestMembersMissingKey()
        {
            Program.ParseCommand("MEMBERS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestAddLowerHappyPath()
        {
            Program.ParseCommand("add key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Add("key", "value"));
        }

        [Fact]
        public void TestAddUpperHappyPath()
        {
            Program.ParseCommand("ADD key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Add("key", "value"));
        }

        [Fact]
        public void TestAddAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("ADD \"key key\" \"value value\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Add("key key", "value value"));
        }

        [Fact]
        public void TestAddExcessParametersFails()
        {
            Program.ParseCommand("ADD key value member", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestAddMissingKeyPath()
        {
            Program.ParseCommand("ADD", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestAddMissingMemberPath()
        {
            Program.ParseCommand("ADD key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestRemoveLowerHappyPath()
        {
            Program.ParseCommand("remove key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Remove("key", "value"));
        }

        [Fact]
        public void TestRemoveUpperHappyPath()
        {
            Program.ParseCommand("REMOVE key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Remove("key", "value"));
        }

        [Fact]
        public void TestRemoveAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("REMOVE \"key key\" \"value value\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Remove("key key", "value value"));
        }

        [Fact]
        public void TestRemoveExcessParametersFails()
        {
            Program.ParseCommand("REMOVE key value member", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestRemoveMissingKeyPath()
        {
            Program.ParseCommand("REMOVE key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestRemoveMissingMemberPath()
        {
            Program.ParseCommand("REMOVE key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestRemoveAllLowerHappyPath()
        {
            Program.ParseCommand("removeall key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.RemoveAll("key"));
        }

        [Fact]
        public void TestRemoveAllUpperHappyPath()
        {
            Program.ParseCommand("REMOVEALL key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.RemoveAll("key"));
        }

        [Fact]
        public void TestRemoveAllAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("REMOVEALL \"key key\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.RemoveAll("key key"));
        }

        [Fact]
        public void TestRemoveAllExcessParametersFails()
        {
            Program.ParseCommand("REMOVEALL key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestRemoveAllMissingKeyPath()
        {
            Program.ParseCommand("REMOVEALL", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestClearLowerHappyPath()
        {
            Program.ParseCommand("clear", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Clear());
        }

        [Fact]
        public void TestClearUpperHappyPath()
        {
            Program.ParseCommand("CLEAR", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Clear());
        }

        [Fact]
        public void TestClearExcessParametersFails()
        {
            Program.ParseCommand("CLEAR key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestKeyExistsLowerHappyPath()
        {
            Program.ParseCommand("keyexists key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.KeyExists("key"));
        }

        [Fact]
        public void TestKeyExistsUpperHappyPath()
        {
            Program.ParseCommand("KEYEXISTS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.KeyExists("key"));
        }

        [Fact]
        public void TestKeyExistsAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("KEYEXISTS \"key key\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.KeyExists("key key"));
        }

        [Fact]
        public void TestKeyExistsExcessParametersFails()
        {
            Program.ParseCommand("KEYEXISTS key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestKeyExistsMissingKeyPath()
        {
            Program.ParseCommand("KEYEXISTS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestMemberExistsLowerHappyPath()
        {
            Program.ParseCommand("memberexists key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.MemberExists("key", "value"));
        }

        [Fact]
        public void TestMemberExistsUpperHappyPath()
        {
            Program.ParseCommand("MEMBEREXISTS key value", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.MemberExists("key", "value"));
        }

        [Fact]
        public void TestMemberExistsAcceptsQuotedValuesPath()
        {
            Program.ParseCommand("MEMBEREXISTS \"key key\" \"value value\"", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.MemberExists("key key", "value value"));
        }

        [Fact]
        public void TestMemberExistsExcessParametersFails()
        {
            Program.ParseCommand("MEMBEREXISTS key value member", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestMemberExistsMissingKeyPath()
        {
            Program.ParseCommand("MEMBEREXISTS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestMemberExistsMissingMemberPath()
        {
            Program.ParseCommand("MEMBEREXISTS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestAllMembersLowerHappyPath()
        {
            Program.ParseCommand("allmembers", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.AllMembers());
        }

        [Fact]
        public void TestAllMembersUpperHappyPath()
        {
            Program.ParseCommand("ALLMEMBERS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.AllMembers());
        }

        [Fact]
        public void TestAllMembersExistsExcessParametersFails()
        {
            Program.ParseCommand("ALLMEMBERS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestItemsLowerHappyPath()
        {
            Program.ParseCommand("ALLMEMBERS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.AllMembers());
        }

        [Fact]
        public void TestItemsUpperHappyPath()
        {
            Program.ParseCommand("ALLMEMBERS", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.AllMembers());
        }

        [Fact]
        public void TestItemsExistsExcessParametersFails()
        {
            Program.ParseCommand("ITEMS key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestListLowerHappyPath()
        {
            Program.ParseCommand("list", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.List());
        }

        [Fact]
        public void TestListUpperHappyPath()
        {
            Program.ParseCommand("LIST", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.List());
        }

        [Fact]
        public void TestListExistsExcessParametersFails()
        {
            Program.ParseCommand("LIST key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestHelpLowerHappyPath()
        {
            Program.ParseCommand("help", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Help());
        }

        [Fact]
        public void TestHelpUpperHappyPath()
        {
            Program.ParseCommand("HELP", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Help());
        }

        [Fact]
        public void TestHelpExistsExcessParametersFails()
        {
            Program.ParseCommand("HELP key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestQuitLowerHappyPath()
        {
            Program.ParseCommand("quit", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Quit());
        }

        [Fact]
        public void TestQuitUpperHappyPath()
        {
            Program.ParseCommand("QUIT", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Quit());
        }

        [Fact]
        public void TestQuitExistsExcessParametersFails()
        {
            Program.ParseCommand("QUIT key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestExitLowerHappyPath()
        {
            Program.ParseCommand("exit", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Exit());
        }

        [Fact]
        public void TestExitUpperHappyPath()
        {
            Program.ParseCommand("EXIT", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Exit());
        }

        [Fact]
        public void TestExitExistsExcessParametersFails()
        {
            Program.ParseCommand("EXIT key", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }

        [Fact]
        public void TestErrorHappyPath()
        {
            Program.ParseCommand("fake", mockDictionaryService.Object);

            mockDictionaryService.Verify(x => x.Error());
        }
    }
}
