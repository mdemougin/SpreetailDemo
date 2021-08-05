using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreetailDemo.Services.Interfaces
{
    public interface IDictionaryService
    {
        List<KeyValuePair<string, string>> Dictionary { get; }
        ILookup<string, string> Lookup { get; }
        string Keys();
        string Members(string key);
        string Add(string key, string member);
        string Remove(string key, string member);
        string RemoveAll(string key);
        string Clear();
        string KeyExists(string key);
        string MemberExists(string key, string member);
        string AllMembers();
        string Items();
        string List();
        string Help();
        string Error();

        string Exit();
        string Quit();
        string GetKeysCommandText();
        string GetMembersCommandText();
        string GetAddCommandText();
        string GetRemoveCommandText();
        string GetRemoveAllCommandText();
        string GetClearCommandText();
        string GetKeyExistsCommandText();
        string GetMemberExistsCommandText();
        string GetAllMembersCommandText();
        string GetItemsCommandText();
        string GetListCommandText();
        string GetHelpCommandText();
    }
}
