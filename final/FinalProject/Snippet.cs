using System.Text.Json;
using System.Text.Json.Nodes;

public class Snippet {
    // Attributes:
    private readonly string _title;
    private readonly string _keyword;
    private readonly string _description;
    private readonly List<string> _body;

    // Constructors:
    public Snippet(string title, string keyword, string description, List<string> body) {
        _title = title;
        _keyword = keyword;
        _description = description;
        _body = body;
    }
    public Snippet(string title, JsonObject jsonData) {
        _title = title;
        _keyword = jsonData["prefix"].ToString();
        _description = jsonData["description"].ToString();
        _body = jsonData["body"].AsArray().Select(line => line.ToString()).ToList();
    }

    // Methods:
    public void DisplaySnippet(Dictionary<string, ConsoleColor> colorKey) {
        _body.ForEach(line => ConsoleUtility.ColorPassage(line, colorKey));
    }
    public void DisplaySnippet() {
        _body.ForEach(Console.WriteLine);
    }
    public KeyValuePair<string, JsonNode> ToJson() {
        JsonNode returnObject = new JsonObject {
            {"prefix", _keyword},
            {"body", JsonValue.Create(_body)},
            {"description", _description}
        };
        return new KeyValuePair<string, JsonNode>(_title, returnObject);
    }
    public string ToShortString() {
        return $"{_keyword}: {_title}";
    }
    public string ToLongString() {
        return $"{_keyword}: {_title}\n > {_description}";
    }
}