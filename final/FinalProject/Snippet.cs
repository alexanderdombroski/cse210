using System.Text.Json;
using System.Text.Json.Nodes;

public class Snippet {
    // Attributes:
    private readonly string _title;
    private readonly string _keyword;
    private readonly string _description;
    private List<string> _body;
    private readonly string _scope;

    // Constructors:
    public Snippet(string title, string keyword, string description, List<string> body, string scope) {
        // Initialized snippet data from a manual input
        _title = title;
        _keyword = keyword;
        _description = description;
        _body = body;
        _scope = scope;
    }
    public Snippet(string title, JsonObject jsonData) {
        // Initialize snippet data from a file
        _title = title;
        _keyword = jsonData["prefix"].ToString();
        _description = jsonData["description"].ToString();
        _body = jsonData["body"].AsArray().Select(line => line.ToString()).ToList();
        if (jsonData.ContainsKey("scope")) {
            _scope = jsonData["scope"].ToString();
        } else {
            _scope = null;
        }
    }
    public Snippet(Snippet snippet) {
        // Duplication constructor
        _title = snippet._title;
        _keyword = snippet._keyword;
        _description = snippet._description;
        _body = snippet._body;
        _scope = snippet._scope;
    }

    // Methods:
    public void DisplaySnippet(Dictionary<string, ConsoleColor> colorKey) {
        // Display a snippet in color
        _body.ForEach(line => ConsoleUtility.ColorPassage(line, colorKey));
    }
    public KeyValuePair<string, JsonNode> ToJson() {
        // Gets snippet data in Json format
        JsonNode returnObject = new JsonObject {
            {"prefix", _keyword},
            {"body", JsonValue.Create(_body)},
            {"description", _description},
        };
        if (_scope != null) {
            returnObject["scope"] = _scope;
        }
        return new KeyValuePair<string, JsonNode>(_title, returnObject);
    }
    public string ToShortString() {
        // diplay option for a choice menu
        return $"{_keyword}: {_title}";
    }
    public string ToLongString() {
        // display optinon for a detailed menu
        return $"{_keyword}: {_title}\n > {_description}";
    }
    public void UpdateBody(List<string> body) {
        _body = body;
    }
    public List<string> GetBody() {
        return _body;
    }
}