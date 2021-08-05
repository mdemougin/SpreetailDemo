using SpreetailDemo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreetailDemo.Implementations
{
    public class DictionaryService : IDictionaryService
    {
        /// <summary>
        /// This is our "dictionary"
        /// Dictionaries don't actually work for the requirements of the assignment, so we're using a list of keyvaluepairs to simulate
        /// This will be a singleton that will store our data for the program
        /// </summary>
        private static readonly List<KeyValuePair<string, string>> _singletonDictionary = new List<KeyValuePair<string, string>>();
        
        /// <summary>
        /// Property for accessing the dictionary
        /// </summary>
        public virtual List<KeyValuePair<string, string>> Dictionary 
        {
            get { return _singletonDictionary; }
        }

        /// <summary>
        /// Lookup object for easy organizational access to the dictionary.
        /// </summary>
        public virtual ILookup<string, string> Lookup
        {
            get
            {
                return Dictionary.ToLookup(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Lists distinct keys in the dictionary
        /// </summary>
        /// <returns>List of keys in the dictionary, 1) key format or (empty set) if no keys exist in the dictionary.</returns>
        public virtual string Keys()
        {
            StringBuilder keysCommandResult = new StringBuilder();

            if (Dictionary.Count == 0)
            {
                keysCommandResult.AppendLine("(empty set)");
            }
            else
            {
                int index = 1;

                // Ensure only distinct keys are selected.
                Dictionary.Select(x => x.Key).Distinct().ToList().ForEach((x) =>
                {
                    keysCommandResult.AppendLine($"{index++}) {x}");
                });
            }

            return keysCommandResult.ToString();
        }

        /// <summary>
        /// Returns all members of a given key in the dictionary
        /// </summary>
        /// <param name="key">String key for member retrieval</param>
        /// <returns>List of members of the given key in 1) member format, or an error if an invalid key is supplied</returns>
        public virtual string Members(string key)
        {
            StringBuilder membersCommandResult = new StringBuilder();

            // This will organize all of our members from the single key into a key/value list
            List<IGrouping<string, string>> memberGroup = Lookup.Where(x => x.Key == key).ToList();

            if (string.IsNullOrWhiteSpace(key))
            {
                membersCommandResult.AppendLine(") ERROR, no key was supplied");
            }
            else if (memberGroup.Count == 0)
            {
                membersCommandResult.AppendLine($") ERROR, key {key} does not exist");
            }
            else
            {
                int index = 1;

                membersCommandResult.AppendLine($"Members for {key}");

                foreach (var item in memberGroup)
                {
                    foreach (var value in item)
                    {
                        membersCommandResult.AppendLine($"{index++}) {value}");
                    }
                }
            }
            
            return membersCommandResult.ToString();
        }

        /// <summary>
        /// Adds the supplied key and member combination to the dictionary
        /// </summary>
        /// <param name="key">String key for dictionary addition</param>
        /// <param name="member">String member for dictionary addition</param>
        /// <returns>Message indicating successful addition to the dictionary, 
        /// or error message if invalid or duplicate key/member combination is supplied</returns>
        public virtual string Add(string key, string member)
        {
            StringBuilder addCommandResult = new StringBuilder();

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(member))
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    addCommandResult.AppendLine(") ERROR, no key was supplied");
                }

                if (string.IsNullOrWhiteSpace(member))
                {
                    addCommandResult.AppendLine(") ERROR, no member was supplied");
                }
            }
            else
            {
                if (Lookup.Contains(key) && Lookup[key].Any(x => x == member))
                {
                    addCommandResult.AppendLine($") ERROR member {member} already exists for key {key}");
                }
                else
                {
                    Dictionary.Add(new KeyValuePair<string, string>(key, member));
                    addCommandResult.AppendLine($") Added member {member} to key {key}");
                }
            }    
            
            return addCommandResult.ToString();
        }

        /// <summary>
        /// Removes a key/member combination from the dictionary
        /// </summary>
        /// <param name="key">String key for dicionary removal</param>
        /// <param name="member">String member for dictionary removal</param>
        /// <returns>Message indicating successful removal from the dictionary, 
        /// or error message if invalid key/member combination is supplied</returns>
        public virtual string Remove(string key, string member)
        {
            StringBuilder removeCommandResult = new StringBuilder();

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(member))
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    removeCommandResult.AppendLine(") ERROR, no key was supplied");
                }

                if (string.IsNullOrWhiteSpace(member))
                {
                    removeCommandResult.AppendLine(") ERROR, no member was supplied");
                }
            }
            else
            {
               if(!Lookup.Contains(key))
                {
                    removeCommandResult.AppendLine($") ERROR Dictionary contains no entry for key {key}");
                }
                else if (!Lookup[key].Any(x => x == member))
                {
                    removeCommandResult.AppendLine($") ERROR member {member} does not exist for key {key}");
                }
                else
                {
                    Dictionary.Remove(new KeyValuePair<string, string>(key, member));
                    removeCommandResult.AppendLine($") Removed member {member} from key {key}");
                }
            }

            return removeCommandResult.ToString();
        }

        /// <summary>
        /// Removes all members from the supplied key
        /// </summary>
        /// <param name="key">String key to remove all members from</param>
        /// <returns>Message indicating successful removal of all members of the specified key from the dictionary, 
        /// or error message if invalid key/member combination is supplied</returns>
        public virtual string RemoveAll(string key)
        {
            StringBuilder removeAllCommandResult = new StringBuilder();

            if (string.IsNullOrWhiteSpace(key))
            {
                removeAllCommandResult.AppendLine(") ERROR, no key was supplied");
            }
            else if (!Lookup.Contains(key))
            {
                removeAllCommandResult.AppendLine($") ERROR Dictionary contains no entry for key {key}");
            }
            else
            {
                Dictionary.RemoveAll(x => x.Key == key);
                removeAllCommandResult.AppendLine($") Removed key {key} from Dictionary");
            }

            return removeAllCommandResult.ToString();
        }

        /// <summary>
        /// Removes all key/member pairs from the dictionary
        /// </summary>
        /// <returns>Returns a success message after removal</returns>
        public virtual string Clear()
        {
            Dictionary.Clear();
            return ") Cleared\r\n";
        }

        /// <summary>
        /// Detects whether a supplied key exists in the dictionary
        /// </summary>
        /// <param name="key">String key to check for existence</param>
        /// <returns>True message if key exists, false message if key does not exist, error message if invalid key is supplied</returns>
        public virtual string KeyExists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return ") ERROR, no key was supplied\r\n";
            }

            bool exists = Dictionary.Where(x => x.Key == key).Count() > 0;

            return exists ? ") true\r\n" : ") false\r\n";
        }

        /// <summary>
        /// Detects whether a supplied key/member exists in the dictionary
        /// </summary>
        /// <param name="key">String key to check for member existence on</param>
        /// <param name="member">String member to check for existence</param>
        /// <returns>True message if key/member exists, false message if key/member does not exist, error message if invalid key and/or member is supplied</returns>
        public virtual string MemberExists(string key, string member)
        {
            StringBuilder memberExistsResult = new StringBuilder();

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(member))
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    memberExistsResult.AppendLine(") ERROR, no key was supplied");
                }

                if (string.IsNullOrWhiteSpace(member))
                {
                    memberExistsResult.AppendLine(") ERROR, no member was supplied");
                }
            }
            else
            {
                bool exists = Dictionary.Where(x => x.Key == key && x.Value == member).Count() > 0;
                memberExistsResult.AppendLine(exists ? ") true" : ") false");
            }

            return memberExistsResult.ToString();
        }

        /// <summary>
        /// Lists every member present in the dictionary
        /// </summary>
        /// <returns>Returns a string list of the members in the dictionary in 1) member format, returns (empty set) if no members exist</returns>
        public virtual string AllMembers()
        {
            StringBuilder allMembersCommandResult = new StringBuilder();

            int index = 1;

            if (Dictionary.Count() == 0)
            {
                return "(empty set)\r\n";
            }

            Dictionary.ForEach((x) =>
            {
                allMembersCommandResult.AppendLine($"{index++}) {x.Value}");
            });

            return allMembersCommandResult.ToString();
        }

        /// <summary>
        /// Lists every key/member combination in the dictionary
        /// </summary>
        /// <returns>Returns a string list of the keys/members in the dictionary in 1) key: member format
        /// returns (empty set) if no key/member combinations exist</returns>
        public virtual string Items()
        {
            StringBuilder itemsCommandResult = new StringBuilder();

            if (Dictionary.Count() == 0)
            {
                return "(empty set)\r\n";
            }

            int index = 1;

            Dictionary.ForEach((x) =>
            {
                itemsCommandResult.AppendLine($"{index++}) {x.Key}: {x.Value}");
            });

            return itemsCommandResult.ToString();
        }

        /// <summary>
        /// Returns a list of available commands and their formats
        /// </summary>
        /// <returns>Returns a list of available commands and their formats</returns>
        public virtual string List()
        {
            StringBuilder listCommandText = new StringBuilder();
            listCommandText.Append(GetAddCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetAllMembersCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetClearCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetHelpCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetKeyExistsCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetKeysCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetItemsCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetListCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetMemberExistsCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetMembersCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetRemoveCommandText());
            listCommandText.AppendLine();
            listCommandText.Append(GetRemoveAllCommandText());
            listCommandText.AppendLine();
            listCommandText.AppendLine("Type QUIT or EXIT to exit");

            return listCommandText.ToString();
        }

        /// <summary>
        /// Returns a list of available commands and their formats
        /// </summary>
        /// <returns>Returns a list of available commands and their formats</returns>
        public virtual string Help()
        {
            return List();
        }

        /// <summary>
        /// Notes that program is being exited
        /// </summary>
        /// <returns>Returns a string noting that program is being exited.</returns>
        public virtual string Exit()
        {
            return "Exiting";
        }

        /// <summary>
        /// Notes that program is being quit
        /// </summary>
        /// <returns>Returns a string noting that program is being quit.</returns>
        public virtual string Quit()
        {
            return "Quitting";
        }

        /// <summary>
        /// Error handling
        /// </summary>
        /// <returns>Returns a string indicating an error has occurred and the list of available command formats</returns>
        public virtual string Error()
        {
            StringBuilder errorCommandText = new StringBuilder();
            errorCommandText.AppendLine("The command entered was not one of the valid commands");
            errorCommandText.AppendLine("Please select from one of the valid commands below");
            errorCommandText.Append(List());

            return errorCommandText.ToString();
        }

        /// <summary>
        /// Format for keys command
        /// </summary>
        /// <returns>String of the format for the keys command</returns>
        public virtual string GetKeysCommandText()
        {
            StringBuilder keysCommandText = new StringBuilder();
            keysCommandText.AppendLine("KEYS");
            keysCommandText.AppendLine("Lists the keys in the dictionary in an indeterminate order");
            keysCommandText.AppendLine("Returns empty set if no keys exist in the dictionary");

            return keysCommandText.ToString();
        }

        /// <summary>
        /// Format for members command
        /// </summary>
        /// <returns>String of the format for the members command</returns>
        public virtual string GetMembersCommandText()
        {
            StringBuilder membersCommandText = new StringBuilder();
            membersCommandText.AppendLine("MEMBERS <key>");
            membersCommandText.AppendLine("Lists the members of the dictionary belonging to the supplied key, in an indeterinate order");
            membersCommandText.AppendLine("Returns an error if the supplied key does not exist");

            return membersCommandText.ToString();
        }

        /// <summary>
        /// Format for add command
        /// </summary>
        /// <returns>String of the format for the add command</returns>
        public virtual string GetAddCommandText()
        {
            StringBuilder addCommandText = new StringBuilder();
            addCommandText.AppendLine("ADD <key> <member>");
            addCommandText.AppendLine("Adds the key with the supplied value. Surround either the key or the value with quotes to allow white space");
            addCommandText.AppendLine("Returns an error if the supplied member already exists on the supplied key");

            return addCommandText.ToString();
        }

        /// <summary>
        /// Format for remove command
        /// </summary>
        /// <returns>String of the format for the remove command</returns>
        public virtual string GetRemoveCommandText()
        {
            StringBuilder removeCommandText = new StringBuilder();
            removeCommandText.AppendLine("REMOVE <key> <member>");
            removeCommandText.AppendLine("Removes the supplied member from the supplied key");
            removeCommandText.AppendLine("Returns an error if the supplied member does not exist on the supplied key or the supplied key does not exist.");

            return removeCommandText.ToString();
        }

        /// <summary>
        /// Format for removeall command
        /// </summary>
        /// <returns>String of the format for the removeall command</returns>
        public virtual string GetRemoveAllCommandText()
        {
            StringBuilder removeAllCommandText = new StringBuilder();
            removeAllCommandText.AppendLine("REMOVEALL <key>");
            removeAllCommandText.AppendLine("Removes all members from the supplied key and removes the key from the dictionary");
            removeAllCommandText.AppendLine("Returns an error if the supplied key does not exist");

            return removeAllCommandText.ToString();
        }

        /// <summary>
        /// Format for clear command
        /// </summary>
        /// <returns>String of the format for the clear command</returns>
        public virtual string GetClearCommandText()
        {
            StringBuilder clearCommandText = new StringBuilder();
            clearCommandText.AppendLine("CLEAR");
            clearCommandText.AppendLine("Removes all keys and members");
            clearCommandText.AppendLine("Returns Cleared status message");

            return clearCommandText.ToString();
        }

        /// <summary>
        /// Format for keyexists command
        /// </summary>
        /// <returns>String of the format for the keyexists command</returns>
        public virtual string GetKeyExistsCommandText()
        {
            StringBuilder keyExistsCommandText = new StringBuilder();
            keyExistsCommandText.AppendLine("KEYEXISTS <key>");
            keyExistsCommandText.AppendLine("Returns whether the supplied key is present in the dictionary.");

            return keyExistsCommandText.ToString();
        }

        /// <summary>
        /// Format for memberexists command
        /// </summary>
        /// <returns>String of the format for the memberexists command</returns>
        public virtual string GetMemberExistsCommandText()
        {
            StringBuilder memberExistsCommandText = new StringBuilder();
            memberExistsCommandText.AppendLine("MEMBEREXISTS <key> <member>");
            memberExistsCommandText.AppendLine("Returns whether the supplied member exists on the supplied key");
            memberExistsCommandText.AppendLine("Also returns false if the supplied key does not exist");

            return memberExistsCommandText.ToString();
        }

        /// <summary>
        /// Format for allmembers command
        /// </summary>
        /// <returns>String of the format for the allmembers command</returns>
        public virtual string GetAllMembersCommandText()
        {
            StringBuilder allMembersCommandText = new StringBuilder();
            allMembersCommandText.AppendLine("ALLMEMBERS");
            allMembersCommandText.AppendLine("Prints the list of dictionary members");

            return allMembersCommandText.ToString();
        }

        /// <summary>
        /// Format for items command
        /// </summary>
        /// <returns>String of the format for the items command</returns>
        public virtual string GetItemsCommandText()
        {
            StringBuilder itemsCommandText = new StringBuilder();
            itemsCommandText.AppendLine("ITEMS");
            itemsCommandText.AppendLine("Returns a list of all of the keys and members in an indeterminate order");

            return itemsCommandText.ToString();
        }

        /// <summary>
        /// Format for list command
        /// </summary>
        /// <returns>String of the format for the list command</returns>
        public virtual string GetListCommandText()
        {
            StringBuilder listCommandText = new StringBuilder();
            listCommandText.AppendLine("LIST");
            listCommandText.AppendLine("Returns a list of available commands");

            return listCommandText.ToString();
        }

        /// <summary>
        /// Format for help command
        /// </summary>
        /// <returns>String of the format for the help command</returns>
        public virtual string GetHelpCommandText()
        {
            StringBuilder helpCommandText = new StringBuilder();
            helpCommandText.AppendLine("HELP");
            helpCommandText.AppendLine("Returns a list of available commands");

            return helpCommandText.ToString();
        }
    }
}
