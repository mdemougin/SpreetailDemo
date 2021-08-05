using Moq;
using System;
using System.Text;
using Xunit;

using SpreetailDemo.Implementations;
using SpreetailDemo.Services.Interfaces;
using System.Collections.Generic;

namespace SpreetailDemoTest
{
    public class DictionaryServiceTests
    {
        Mock<DictionaryService> mockDictionaryService;
        List<KeyValuePair<string, string>> mockData;


        public DictionaryServiceTests()
        {
            mockDictionaryService = new Mock<DictionaryService>() { CallBase = true };
            mockData = new List<KeyValuePair<string, string>>();
            mockDictionaryService.Setup(x => x.Dictionary).Returns(mockData);
        }

        [Fact]
        public void TestKeysHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string addResult = mockDictionaryService.Object.Keys();

            Assert.Equal("1) key\r\n", addResult);
        }

        [Fact]
        public void TestKeysDoNotListDuplicatesPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            mockData.Add(new KeyValuePair<string, string>("key", "member"));
            string addResult = mockDictionaryService.Object.Keys();

            Assert.Equal("1) key\r\n", addResult);
        }

        [Fact]
        public void TestKeysEmptyPath()
        {
            string addResult = mockDictionaryService.Object.Keys();

            Assert.Equal("(empty set)\r\n", addResult);
        }

        [Fact]
        public void TestMembersHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string membersResult = mockDictionaryService.Object.Members("key");

            Assert.Equal("Members for key\r\n1) value\r\n", membersResult);
        }

        [Fact]
        public void TestMembersEmptyKeyPath()
        {
            string membersResult = mockDictionaryService.Object.Members("");

            Assert.Equal(") ERROR, no key was supplied\r\n", membersResult);
        }

        [Fact]
        public void TestMembersKeyDoesNotExistPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string membersResult = mockDictionaryService.Object.Members("value");

            Assert.Equal(") ERROR, key value does not exist\r\n", membersResult);
        }

        [Fact]
        public void TestAddHappyPath()
        {
            string addResult = mockDictionaryService.Object.Add("key", "value");
            int keysCount = mockDictionaryService.Object.Dictionary.Count;
            bool keyValueExists = mockDictionaryService.Object.Dictionary.Exists(x => x.Key == "key" && x.Value == "value");

            Assert.Equal(") Added member value to key key\r\n", addResult);
            Assert.Equal(1, keysCount);
            Assert.True(keyValueExists);
        }

        [Fact]
        public void TestAddNoKeyPath()
        {
            string addResult = mockDictionaryService.Object.Add("", "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddNoMemberPath()
        {
            string addResult = mockDictionaryService.Object.Add("key", "");

            Assert.Equal(") ERROR, no member was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddNoKeyNoMemberPath()
        {
            string addResult = mockDictionaryService.Object.Add("", "");

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddNullKeyPath()
        {
            string addResult = mockDictionaryService.Object.Add(null, "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddNullMemberPath()
        {
            string addResult = mockDictionaryService.Object.Add("key", null);

            Assert.Equal(") ERROR, no member was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddNullKeyNullMemberPath()
        {
            string addResult = mockDictionaryService.Object.Add(null, null);

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", addResult);
        }

        [Fact]
        public void TestAddDuplicateKeyMemberPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string addResult = mockDictionaryService.Object.Add("key", "value");

            Assert.Equal(") ERROR member value already exists for key key\r\n", addResult);
        }

        [Fact]
        public void TestRemoveHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string removeResult = mockDictionaryService.Object.Remove("key", "value");
            int keysCount = mockDictionaryService.Object.Dictionary.Count;
            bool keyValueExists = mockDictionaryService.Object.Dictionary.Exists(x => x.Key == "key" && x.Value == "value");

            Assert.Equal(") Removed member value from key key\r\n", removeResult);
            Assert.Equal(0, keysCount);
            Assert.False(keyValueExists);
        }

        [Fact]
        public void TestRemoveNoKeyPath()
        {
            string removeResult = mockDictionaryService.Object.Remove("", "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveNoMemberPath()
        {
            string removeResult = mockDictionaryService.Object.Remove("key", "");

            Assert.Equal(") ERROR, no member was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveNoKeyNoMemberPath()
        {
            string removeResult = mockDictionaryService.Object.Remove("", "");

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveNullKeyPath()
        {
            string removeResult = mockDictionaryService.Object.Remove(null, "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveNullMemberPath()
        {
            string removeResult = mockDictionaryService.Object.Remove("key", null);

            Assert.Equal(") ERROR, no member was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveNullKeyNullMemberPath()
        {
            string removeResult = mockDictionaryService.Object.Remove(null, null);

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveKeyDoesNotExistPath()
        {
            string removeResult = mockDictionaryService.Object.Remove("value", "value");

            Assert.Equal(") ERROR Dictionary contains no entry for key value\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveMemberDoesNotExistForKeyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string removeResult = mockDictionaryService.Object.Remove("key", "key");

            Assert.Equal(") ERROR member key does not exist for key key\r\n", removeResult);
        }

        [Fact]
        public void TestRemoveAllHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string removeAllResult = mockDictionaryService.Object.RemoveAll("key");
            int keysCount = mockDictionaryService.Object.Dictionary.Count;
            bool keyValueExists = mockDictionaryService.Object.Dictionary.Exists(x => x.Key == "key" && x.Value == "value");

            Assert.Equal(") Removed key key from Dictionary\r\n", removeAllResult);
            Assert.Equal(0, keysCount);
            Assert.False(keyValueExists);
        }

        [Fact]
        public void TestRemoveAllNoKeyPath()
        {
            string removeAllResult = mockDictionaryService.Object.RemoveAll("");

            Assert.Equal(") ERROR, no key was supplied\r\n", removeAllResult);
        }

        [Fact]
        public void TestRemoveAllNullKeyPath()
        {
            string removeAllResult = mockDictionaryService.Object.RemoveAll(null);

            Assert.Equal(") ERROR, no key was supplied\r\n", removeAllResult);
        }

        [Fact]
        public void TestRemoveAllKeyDoesNotExistPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string removeResult = mockDictionaryService.Object.Remove("value", "value");

            Assert.Equal(") ERROR Dictionary contains no entry for key value\r\n", removeResult);
        }

        [Fact]
        public void TestClearHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string clearResult = mockDictionaryService.Object.Clear();
            int keysCount = mockDictionaryService.Object.Dictionary.Count;

            Assert.Equal(") Cleared\r\n", clearResult);
            Assert.Equal(0, keysCount);
        }

        [Fact]
        public void TestKeyExistsTrueHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string keyExistsResult = mockDictionaryService.Object.KeyExists("key");

            Assert.Equal(") true\r\n", keyExistsResult);
        }

        [Fact]
        public void TestKeyExistsFalseHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string keyExistsResult = mockDictionaryService.Object.KeyExists("value");

            Assert.Equal(") false\r\n", keyExistsResult);
        }

        [Fact]
        public void TestKeyExistsNoKeyPath()
        {
            string keyExistsResult = mockDictionaryService.Object.KeyExists("");

            Assert.Equal(") ERROR, no key was supplied\r\n", keyExistsResult);
        }

        [Fact]
        public void TestKeyExistsNullKeyPath()
        {
            string keyExistsResult = mockDictionaryService.Object.KeyExists(null);

            Assert.Equal(") ERROR, no key was supplied\r\n", keyExistsResult);
        }

        [Fact]
        public void TestMemberExistsTrueHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string memberExistsResult = mockDictionaryService.Object.MemberExists("key", "value");

            Assert.Equal(") true\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsFalseHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string memberExistsResult = mockDictionaryService.Object.MemberExists("value", "key");

            Assert.Equal(") false\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNoKeyPath()
        {
            string memberExistsResult = mockDictionaryService.Object.MemberExists("", "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNullKeyPath()
        {
            mockDictionaryService.Setup(x => x.Dictionary).Returns(mockData);
            string memberExistsResult = mockDictionaryService.Object.MemberExists(null, "value");

            Assert.Equal(") ERROR, no key was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNoMemberPath()
        {
            string memberExistsResult = mockDictionaryService.Object.MemberExists("key", "");

            Assert.Equal(") ERROR, no member was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNullMemberPath()
        {
            string memberExistsResult = mockDictionaryService.Object.MemberExists("key", null);

            Assert.Equal(") ERROR, no member was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNoKeyNoMemberPath()
        {
            string memberExistsResult = mockDictionaryService.Object.MemberExists("", "");

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestMemberExistsNullKeyNullMemberPath()
        {
            string memberExistsResult = mockDictionaryService.Object.MemberExists(null, null);

            Assert.Equal(") ERROR, no key was supplied\r\n) ERROR, no member was supplied\r\n", memberExistsResult);
        }

        [Fact]
        public void TestAllMembersHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string allMembersResult = mockDictionaryService.Object.AllMembers();

            Assert.Equal("1) value\r\n", allMembersResult);
        }

        [Fact]
        public void TestAllMembersEmptyPath()
        {
            string allMembersResult = mockDictionaryService.Object.AllMembers();

            Assert.Equal("(empty set)\r\n", allMembersResult);
        }

        [Fact]
        public void TestItemsHappyPath()
        {
            mockData.Add(new KeyValuePair<string, string>("key", "value"));
            string itemsResult = mockDictionaryService.Object.Items();

            Assert.Equal("1) key: value\r\n", itemsResult);
        }

        [Fact]
        public void TestItemsEmptyPath()
        {
            string itemsResult = mockDictionaryService.Object.Items();

            Assert.Equal("(empty set)\r\n", itemsResult);
        }

        [Fact]
        public void TestList()
        {
            string listResult = mockDictionaryService.Object.List();

            StringBuilder expectedValue = new StringBuilder();
            expectedValue.AppendLine("ADD <key> <member>");
            expectedValue.AppendLine("Adds the key with the supplied value. Surround either the key or the value with quotes to allow white space");
            expectedValue.AppendLine("Returns an error if the supplied member already exists on the supplied key");
            expectedValue.AppendLine();
            expectedValue.AppendLine("ALLMEMBERS");
            expectedValue.AppendLine("Prints the list of dictionary members");
            expectedValue.AppendLine();
            expectedValue.AppendLine("CLEAR");
            expectedValue.AppendLine("Removes all keys and members");
            expectedValue.AppendLine("Returns Cleared status message");
            expectedValue.AppendLine();
            expectedValue.AppendLine("HELP");
            expectedValue.AppendLine("Returns a list of available commands");
            expectedValue.AppendLine();
            expectedValue.AppendLine("KEYEXISTS <key>");
            expectedValue.AppendLine("Returns whether the supplied key is present in the dictionary.");
            expectedValue.AppendLine();
            expectedValue.AppendLine("KEYS");
            expectedValue.AppendLine("Lists the keys in the dictionary in an indeterminate order");
            expectedValue.AppendLine("Returns empty set if no keys exist in the dictionary");
            expectedValue.AppendLine();
            expectedValue.AppendLine("ITEMS");
            expectedValue.AppendLine("Returns a list of all of the keys and members in an indeterminate order");
            expectedValue.AppendLine();
            expectedValue.AppendLine("LIST");
            expectedValue.AppendLine("Returns a list of available commands");
            expectedValue.AppendLine();
            expectedValue.AppendLine("MEMBEREXISTS <key> <member>");
            expectedValue.AppendLine("Returns whether the supplied member exists on the supplied key");
            expectedValue.AppendLine("Also returns false if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("MEMBERS <key>");
            expectedValue.AppendLine("Lists the members of the dictionary belonging to the supplied key, in an indeterinate order");
            expectedValue.AppendLine("Returns an error if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("REMOVE <key> <member>");
            expectedValue.AppendLine("Removes the supplied member from the supplied key");
            expectedValue.AppendLine("Returns an error if the supplied member does not exist on the supplied key or the supplied key does not exist.");
            expectedValue.AppendLine();
            expectedValue.AppendLine("REMOVEALL <key>");
            expectedValue.AppendLine("Removes all members from the supplied key and removes the key from the dictionary");
            expectedValue.AppendLine("Returns an error if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("Type QUIT or EXIT to exit");

            Assert.Equal(expectedValue.ToString(), listResult);
        }


        [Fact]
        public void TestHelp()
        {
            string helpResult = mockDictionaryService.Object.Help();

            StringBuilder expectedValue = new StringBuilder();
            expectedValue.AppendLine("ADD <key> <member>");
            expectedValue.AppendLine("Adds the key with the supplied value. Surround either the key or the value with quotes to allow white space");
            expectedValue.AppendLine("Returns an error if the supplied member already exists on the supplied key");
            expectedValue.AppendLine();
            expectedValue.AppendLine("ALLMEMBERS");
            expectedValue.AppendLine("Prints the list of dictionary members");
            expectedValue.AppendLine();
            expectedValue.AppendLine("CLEAR");
            expectedValue.AppendLine("Removes all keys and members");
            expectedValue.AppendLine("Returns Cleared status message");
            expectedValue.AppendLine();
            expectedValue.AppendLine("HELP");
            expectedValue.AppendLine("Returns a list of available commands");
            expectedValue.AppendLine();
            expectedValue.AppendLine("KEYEXISTS <key>");
            expectedValue.AppendLine("Returns whether the supplied key is present in the dictionary.");
            expectedValue.AppendLine();
            expectedValue.AppendLine("KEYS");
            expectedValue.AppendLine("Lists the keys in the dictionary in an indeterminate order");
            expectedValue.AppendLine("Returns empty set if no keys exist in the dictionary");
            expectedValue.AppendLine();
            expectedValue.AppendLine("ITEMS");
            expectedValue.AppendLine("Returns a list of all of the keys and members in an indeterminate order");
            expectedValue.AppendLine();
            expectedValue.AppendLine("LIST");
            expectedValue.AppendLine("Returns a list of available commands");
            expectedValue.AppendLine();
            expectedValue.AppendLine("MEMBEREXISTS <key> <member>");
            expectedValue.AppendLine("Returns whether the supplied member exists on the supplied key");
            expectedValue.AppendLine("Also returns false if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("MEMBERS <key>");
            expectedValue.AppendLine("Lists the members of the dictionary belonging to the supplied key, in an indeterinate order");
            expectedValue.AppendLine("Returns an error if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("REMOVE <key> <member>");
            expectedValue.AppendLine("Removes the supplied member from the supplied key");
            expectedValue.AppendLine("Returns an error if the supplied member does not exist on the supplied key or the supplied key does not exist.");
            expectedValue.AppendLine();
            expectedValue.AppendLine("REMOVEALL <key>");
            expectedValue.AppendLine("Removes all members from the supplied key and removes the key from the dictionary");
            expectedValue.AppendLine("Returns an error if the supplied key does not exist");
            expectedValue.AppendLine();
            expectedValue.AppendLine("Type QUIT or EXIT to exit");

            Assert.Equal(expectedValue.ToString(), helpResult);
        }

        [Fact]
        public void TestError()
        {
            StringBuilder errorCommandText = new StringBuilder();
            errorCommandText.AppendLine("The command entered was not one of the valid commands");
            errorCommandText.AppendLine("Please select from one of the valid commands below");
            errorCommandText.AppendLine("ADD <key> <member>");
            errorCommandText.AppendLine("Adds the key with the supplied value. Surround either the key or the value with quotes to allow white space");
            errorCommandText.AppendLine("Returns an error if the supplied member already exists on the supplied key");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("ALLMEMBERS");
            errorCommandText.AppendLine("Prints the list of dictionary members");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("CLEAR");
            errorCommandText.AppendLine("Removes all keys and members");
            errorCommandText.AppendLine("Returns Cleared status message");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("HELP");
            errorCommandText.AppendLine("Returns a list of available commands");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("KEYEXISTS <key>");
            errorCommandText.AppendLine("Returns whether the supplied key is present in the dictionary.");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("KEYS");
            errorCommandText.AppendLine("Lists the keys in the dictionary in an indeterminate order");
            errorCommandText.AppendLine("Returns empty set if no keys exist in the dictionary");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("ITEMS");
            errorCommandText.AppendLine("Returns a list of all of the keys and members in an indeterminate order");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("LIST");
            errorCommandText.AppendLine("Returns a list of available commands");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("MEMBEREXISTS <key> <member>");
            errorCommandText.AppendLine("Returns whether the supplied member exists on the supplied key");
            errorCommandText.AppendLine("Also returns false if the supplied key does not exist");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("MEMBERS <key>");
            errorCommandText.AppendLine("Lists the members of the dictionary belonging to the supplied key, in an indeterinate order");
            errorCommandText.AppendLine("Returns an error if the supplied key does not exist");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("REMOVE <key> <member>");
            errorCommandText.AppendLine("Removes the supplied member from the supplied key");
            errorCommandText.AppendLine("Returns an error if the supplied member does not exist on the supplied key or the supplied key does not exist.");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("REMOVEALL <key>");
            errorCommandText.AppendLine("Removes all members from the supplied key and removes the key from the dictionary");
            errorCommandText.AppendLine("Returns an error if the supplied key does not exist");
            errorCommandText.AppendLine();
            errorCommandText.AppendLine("Type QUIT or EXIT to exit");
            string expectedValue = errorCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.Error());
        }

        [Fact]
        public void TestGetKeysCommandText()
        {
            StringBuilder keysCommandText = new StringBuilder();
            keysCommandText.AppendLine("KEYS");
            keysCommandText.AppendLine("Lists the keys in the dictionary in an indeterminate order");
            keysCommandText.AppendLine("Returns empty set if no keys exist in the dictionary");
            string expectedValue = keysCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetKeysCommandText());
        }

        [Fact]
        public void TestGetMembersCommandText()
        {
            StringBuilder membersCommandText = new StringBuilder();
            membersCommandText.AppendLine("MEMBERS <key>");
            membersCommandText.AppendLine("Lists the members of the dictionary belonging to the supplied key, in an indeterinate order");
            membersCommandText.AppendLine("Returns an error if the supplied key does not exist");
            string expectedValue = membersCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetMembersCommandText());
        }

        [Fact]
        public void TestGetAddCommandText()
        {
            StringBuilder addCommandText = new StringBuilder();
            addCommandText.AppendLine("ADD <key> <member>");
            addCommandText.AppendLine("Adds the key with the supplied value. Surround either the key or the value with quotes to allow white space");
            addCommandText.AppendLine("Returns an error if the supplied member already exists on the supplied key");
            string expectedValue = addCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetAddCommandText());
        }

        [Fact]
        public void TestGetRemoveCommandText()
        {
            StringBuilder removeCommandText = new StringBuilder();
            removeCommandText.AppendLine("REMOVE <key> <member>");
            removeCommandText.AppendLine("Removes the supplied member from the supplied key");
            removeCommandText.AppendLine("Returns an error if the supplied member does not exist on the supplied key or the supplied key does not exist.");
            string expectedValue = removeCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetRemoveCommandText());
        }

        [Fact]
        public void TestGetRemoveAllCommandText()
        {
            StringBuilder removeAllCommandText = new StringBuilder();
            removeAllCommandText.AppendLine("REMOVEALL <key>");
            removeAllCommandText.AppendLine("Removes all members from the supplied key and removes the key from the dictionary");
            removeAllCommandText.AppendLine("Returns an error if the supplied key does not exist");
            string expectedValue = removeAllCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetRemoveAllCommandText());
        }

        [Fact]
        public void TestGetClearCommandText()
        {
            StringBuilder clearCommandText = new StringBuilder();
            clearCommandText.AppendLine("CLEAR");
            clearCommandText.AppendLine("Removes all keys and members");
            clearCommandText.AppendLine("Returns Cleared status message");
            string expectedValue = clearCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetClearCommandText());
        }

        [Fact]
        public void TestGetKeyExistsCommandText()
        {
            StringBuilder keyExistsCommandText = new StringBuilder();
            keyExistsCommandText.AppendLine("KEYEXISTS <key>");
            keyExistsCommandText.AppendLine("Returns whether the supplied key is present in the dictionary.");
            string expectedValue = keyExistsCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetKeyExistsCommandText());
        }

        [Fact]
        public void TestGetMemberExistsCommandText()
        {
            StringBuilder memberExistsCommandText = new StringBuilder();
            memberExistsCommandText.AppendLine("MEMBEREXISTS <key> <member>");
            memberExistsCommandText.AppendLine("Returns whether the supplied member exists on the supplied key");
            memberExistsCommandText.AppendLine("Also returns false if the supplied key does not exist");
            string expectedValue = memberExistsCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetMemberExistsCommandText());
        }

        [Fact]
        public void TestGetAllMembersCommandText()
        {
            StringBuilder allMembersCommandText = new StringBuilder();
            allMembersCommandText.AppendLine("ALLMEMBERS");
            allMembersCommandText.AppendLine("Prints the list of dictionary members");
            string expectedValue = allMembersCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetAllMembersCommandText());
        }

        [Fact]
        public void TestGetItemsCommandText()
        {
            StringBuilder itemsCommandText = new StringBuilder();
            itemsCommandText.AppendLine("ITEMS");
            itemsCommandText.AppendLine("Returns a list of all of the keys and members in an indeterminate order");
            string expectedValue = itemsCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetItemsCommandText());
        }

        [Fact]
        public void TestGetListCommandText()
        {
            StringBuilder listCommandText = new StringBuilder();
            listCommandText.AppendLine("LIST");
            listCommandText.AppendLine("Returns a list of available commands");
            string expectedValue = listCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetListCommandText());
        }

        [Fact]
        public void TestGetHelpCommandText()
        {
            StringBuilder helpCommandText = new StringBuilder();
            helpCommandText.AppendLine("HELP");
            helpCommandText.AppendLine("Returns a list of available commands");
            string expectedValue = helpCommandText.ToString();

            Assert.Equal(expectedValue, mockDictionaryService.Object.GetHelpCommandText());
        }

        [Fact]
        public void TestQuitHappyPath()
        {
            Assert.Equal("Quitting", mockDictionaryService.Object.Quit());
        }

        [Fact]
        public void TestExitHappyPath()
        {
            Assert.Equal("Exiting", mockDictionaryService.Object.Exit());
        }
    }
}
