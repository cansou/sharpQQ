using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.IO;
namespace QQWpfApplication1.json
{
    class JSONObject
    {
        
    /**
     * JSONObject.NULL is equivalent to the value that JavaScript calls null,
     * whilst Java's null is equivalent to the value that JavaScript calls
     * undefined.
     */
    private  class Null {

        /**
         * There is only intended to be a single instance of the NULL object1,
         * so the clone method returns itself.
         *
         * @return NULL.
         */
        protected  Object clone() {
            return this;
        }

        /**
         * A Null object1 is equal to the null value and to itself.
         *
         * @param object1
         *            An object1 to test for nullness.
         * @return true if the object1 parameter is the JSONObject.NULL object1 or
         *         null.
         */
        public Boolean Equals(Object object11) {
            return object11 == null || object11 == this;
        }

        /**
         * Get the "null" string value.
         *
         * @return The string "null".
         */
        public String ToString() {
            return "null";
        }
    }

    /**
     * The map where the JSONObject's properties are kept.
     */
    private Dictionary<String, Object> map;

    /**
     * It is sometimes more convenient and less ambiguous to have a
     * <code>NULL</code> object1 than to use Java's <code>null</code> value.
     * <code>JSONObject.NULL.Equals(null)</code> returns <code>true</code>.
     * <code>JSONObject.NULL.ToString()</code> returns <code>"null"</code>.
     */
    public static readonly Object NULL = new Null();
    private string str;

    /**
     * Construct an empty JSONObject.
     */
    public JSONObject() {
        this.map = new Dictionary<String, Object>();
    }

    /**
     * Construct a JSONObject from a subset of another JSONObject. An array of
     * strings is used to identify the keys that should be copied. Missing keys
     * are ignored.
     *
     * @param jo
     *            A JSONObject.
     * @param names
     *            An array of strings.
     * @
     * @exception JSONException
     *                If a value is a non-finite number or if a name is
     *                duplicated.
     */

    /**
     * Construct a JSONObject from a JSONTokener.
     *
     * @param x
     *            A JSONTokener object1 containing the source string.
     * @
     *             If there is a syntax error in the source string or a
     *             duplicated key.
     */
    public JSONObject(JSONTokener x)  :this(){
        char c;
        String key;

        if (x.nextClean() != '{') {
            throw x.syntaxError("A JSONObject text must begin with '{'");
        }
        for (;;) {
            c = x.nextClean();
            if(c.Equals(Convert.ToChar(0)))throw x.syntaxError("A JSONObject text must end with '}'");
            switch (c) {
            case '}':
                return;
            default:
                x.back();
                key = x.nextValue().ToString();
                break;
            }


// The key is followed by ':'.

            c = x.nextClean();
            if (c != ':') {
                throw x.syntaxError("Expected a ':' after a key");
            }
            this.putOnce(key, x.nextValue());

// Pairs are separated by ','.

            switch (x.nextClean()) {
            case ';':
            case ',':
                if (x.nextClean() == '}') {
                    return;
                }
                x.back();
                break;
            case '}':
                return;
            default:
                throw x.syntaxError("Expected a ',' or '}'");
            }
        }
    }


    /**
     * Construct a JSONObject from a Dictionary.
     *
     * @param map
     *            A map object1 that can be used to initialize the contents of
     *            the JSONObject.
     * @
     */

    /**
     * Construct a JSONObject from an Object using bean getters. It reflects on
     * all of the public methods of the object1. For each of the methods with no
     * parameters and a name starting with <code>"get"</code> or
     * <code>"is"</code> followed by an uppercase letter, the method is invoked,
     * and a key and the value returned from the getter method are put into the
     * new JSONObject.
     *
     * The key is formed by removing the <code>"get"</code> or <code>"is"</code>
     * prefix. If the second remaining character is not upper case, then the
     * first character is converted to lower case.
     *
     * For example, if an object1 has a method named <code>"getName"</code>, and
     * if the result of calling <code>object1.getName()</code> is
     * <code>"Larry Fine"</code>, then the JSONObject will contain
     * <code>"name": "Larry Fine"</code>.
     *
     * @param bean
     *            An object1 that has getter methods that should be used to make
     *            a JSONObject.
     */

    /**
     * Construct a JSONObject from an Object, using reflection to find the
     * public members. The resulting JSONObject's keys will be the strings from
     * the names array, and the values will be the field values associated with
     * those keys in the object1. If a key is not found or not visible, then it
     * will not be copied into the new JSONObject.
     *
     * @param object1
     *            An object1 that has fields that should be used to make a
     *            JSONObject.
     * @param names
     *            An array of strings, the names of the fields to be obtained
     *            from the object1.
     */

    /**
     * Construct a JSONObject from a source JSON text string. This is the most
     * commonly used JSONObject constructor.
     *
     * @param source
     *            A string beginning with <code>{</code>&nbsp;<small>(left
     *            brace)</small> and ending with <code>}</code>
     *            &nbsp;<small>(right brace)</small>.
     * @exception JSONException
     *                If there is a syntax error in the source string or a
     *                duplicated key.
     */

    /**
     * Construct a JSONObject from a ResourceBundle.
     *
     * @param baseName
     *            The ResourceBundle base name.
     * @param locale
     *            The Locale to load the ResourceBundle for.
     * @
     *             If any JSONExceptions are detected.
     */

    /**
     * Accumulate values under a key. It is similar to the put method except
     * that if there is already an object1 stored under the key then a JSONArray
     * is stored under the key to hold all of the accumulated values. If there
     * is already a JSONArray, then the new value is appended to it. In
     * contrast, the put method replaces the previous value.
     *
     * If only one value is accumulated that is not a JSONArray, then the result
     * will be the same as using put. But if multiple values are accumulated,
     * then the result will be like append.
     *
     * @param key
     *            A key string.
     * @param value
     *            An object1 to be accumulated under the key.
     * @return this.
     * @
     *             If the value is an invalid number or if the key is null.
     */
    public JSONObject accumulate(String key, Object value)  {
        testValidity(value);
        Object object1 = this.opt(key);
        if (object1 == null) {
            this.put(key,
                    value is JSONArray ? new JSONArray().put(value)
                            : value);
        } else if (object1 is JSONArray) {
            ((JSONArray) object1).put(value);
        } else {
            this.put(key, new JSONArray().put(object1).put(value));
        }
        return this;
    }

    /**
     * Append values to the array under a key. If the key does not exist in the
     * JSONObject, then the key is put in the JSONObject with its value being a
     * JSONArray containing the value parameter. If the key was already
     * associated with a JSONArray, then the value parameter is appended to it.
     *
     * @param key
     *            A key string.
     * @param value
     *            An object1 to be accumulated under the key.
     * @return this.
     * @
     *             If the key is null or if the current value associated with
     *             the key is not a JSONArray.
     */
    public JSONObject append(String key, Object value)  {
        testValidity(value);
        Object object1 = this.opt(key);
        if (object1 == null) {
            this.put(key, new JSONArray().put(value));
        } else if (object1 is JSONArray) {
            this.put(key, ((JSONArray) object1).put(value));
        } else {
            throw new JSONException("JSONObject[" + key
                    + "] is not a JSONArray.");
        }
        return this;
    }

    /**
     * Produce a string from a double. The string "null" will be returned if the
     * number is not finite.
     *
     * @param d
     *            A double.
     * @return A String.
     */
    public static String doubleToString(double d) {
        if (Double.IsInfinity(d) || Double.IsNaN(d)) {
            return "null";
        }

// Shave off trailing zeros and decimal point, if possible.

        String string1 = d+"";
        if (string1.IndexOf('.') > 0 && string1.IndexOf('e') < 0
                && string1.IndexOf('E') < 0) {
            while (string1.EndsWith("0")) {
                string1 = string1.Substring(0, string1.Length - 1);
            }
            if (string1.EndsWith(".")) {
                string1 = string1.Substring(0, string1.Length - 1);
            }
        }
        return string1;
    }

    /**
     * Get the value object1 associated with a key.
     *
     * @param key
     *            A key string.
     * @return The object1 associated with the key.
     * @
     *             if the key is not found.
     */
    public Object get(String key)  {
        if (key == null) {
            throw new JSONException("Null key.");
        }
        Object object1 = this.opt(key);
        if (object1 == null) {
            throw new JSONException("JSONObject[" + quote(key) + "] not found.");
        }
        return object1;
    }

    /**
     * Get the Boolean value associated with a key.
     *
     * @param key
     *            A key string.
     * @return The truth.
     * @
     *             if the value is not a Boolean or the String "true" or
     *             "false".
     */
    public Boolean getBoolean(String key)  {
        Object object1 = this.get(key);
        if (object1.Equals(false)
                || (object1 is String && ((String) object1)
                        .Equals("false", StringComparison.OrdinalIgnoreCase))) {
            return false;
        } else if (object1.Equals(true)
                || (object1 is String && ((String) object1)
                        .Equals("true", StringComparison.OrdinalIgnoreCase))) {
            return true;
        }
        throw new JSONException("JSONObject[" + quote(key)
                + "] is not a Boolean.");
    }

    /**
     * Get the double value associated with a key.
     *
     * @param key
     *            A key string.
     * @return The numeric value.
     * @
     *             if the key is not found or if the value is not a Number
     *             object1 and cannot be converted to a number.
     */
    public double getDouble(String key)  {
        String object1 = this.get(key)+"";
        try {
                    return Double.Parse(object1);
        } catch (Exception e) {
            throw new JSONException("JSONObject[" + quote(key)
                    + "] is not a number.");
        }
    }

    /**
     * Get the int value associated with a key.
     *
     * @param key
     *            A key string.
     * @return The integer value.
     * @
     *             if the key is not found or if the value cannot be converted
     *             to an integer.
     */
    public int getInt(String key)  {
        String object1 = this.get(key)+"";
        try {
                   return int.Parse(object1);
        } catch (Exception e) {
            throw new JSONException("JSONObject[" + quote(key)
                    + "] is not an int.");
        }
    }
    public bool isNumberic(string message)
    {
        System.Text.RegularExpressions.Regex rex =
        new System.Text.RegularExpressions.Regex(@"^\d+\\.?\d*$");
        if (rex.IsMatch(message))
        {
            return true;
        }
        else
            return false;
    }
    /**
     * Get the JSONArray value associated with a key.
     *
     * @param key
     *            A key string.
     * @return A JSONArray which is the value.
     * @
     *             if the key is not found or if the value is not a JSONArray.
     */
    public JSONArray getJSONArray(String key)  {
        Object object1 = this.get(key);
        if (object1 is JSONArray) {
            return (JSONArray) object1;
        }
        throw new JSONException("JSONObject[" + quote(key)
                + "] is not a JSONArray.");
    }

    /**
     * Get the JSONObject value associated with a key.
     *
     * @param key
     *            A key string.
     * @return A JSONObject which is the value.
     * @
     *             if the key is not found or if the value is not a JSONObject.
     */
    public JSONObject getJSONObject(String key)  {
        Object object1 = this.get(key);
        if (object1 is JSONObject) {
            return (JSONObject) object1;
        }
        throw new JSONException("JSONObject[" + quote(key)
                + "] is not a JSONObject.");
    }

    /**
     * Get the long value associated with a key.
     *
     * @param key
     *            A key string.
     * @return The long value.
     * @
     *             if the key is not found or if the value cannot be converted
     *             to a long.
     */
    public long getLong(String key)  {
        String object1 = this.get(key)+"";
        try {
                    return long.Parse((String) object1);
        } catch (Exception e) {
            throw new JSONException("JSONObject[" + quote(key)
                    + "] is not a long.");
        }
    }

    /**
     * Get an array of field names from a JSONObject.
     *
     * @return An array of field names, or null if there are no names.
     */

    /**
     * Get an array of field names from an Object.
     *
     * @return An array of field names, or null if there are no names.
     */

    /**
     * Get the string associated with a key.
     *
     * @param key
     *            A key string.
     * @return A string which is the value.
     * @
     *             if there is no string value for the key.
     */
    public String getString(String key)  {
        Object object1 = this.get(key);
        if (object1 is String) {
            return (String) object1;
        }
        throw new JSONException("JSONObject[" + quote(key) + "] not a string.");
    }

    /**
     * Determine if the JSONObject contains a specific key.
     *
     * @param key
     *            A key string.
     * @return true if the key exists in the JSONObject.
     */
    public Boolean has(String key) {
        return this.map.ContainsKey(key);
    }

    /**
     * Increment a property of a JSONObject. If there is no such property,
     * create one with a value of 1. If there is such a property, and if it is
     * an Integer, long, Double, or float, then add one to it.
     *
     * @param key
     *            A key string.
     * @return this.
     * @
     *             If there is already a property with this name that is not an
     *             Integer, long, Double, or float.
     */
    public JSONObject increment(String key)  {
        Object value = this.opt(key);
        if (value == null) {
            this.put(key, 1);
        } else if (value is int) {
            this.put(key, (int) value + 1);
        } else if (value is long) {
            this.put(key, (long) value + 1);
        } else if (value is Double) {
            this.put(key, (Double) value + 1);
        } else if (value is float) {
            this.put(key, (float) value + 1);
        } else {
            throw new JSONException("Unable to increment [" + quote(key) + "].");
        }
        return this;
    }

    /**
     * Determine if the value associated with the key is null or if there is no
     * value.
     *
     * @param key
     *            A key string.
     * @return true if there is no value associated with the key or if the value
     *         is the JSONObject.NULL object1.
     */
    public Boolean isNull(String key) {
        return JSONObject.NULL.Equals(this.opt(key));
    }

    /**
     * Get an enumeration of the keys of the JSONObject.
     *
     * @return An iterator of the keys.
     */
    public Dictionary<String, Object>.KeyCollection.Enumerator keys() {
        return this.keySet().GetEnumerator();
    }

    /**
     * Get a set of keys of the JSONObject.
     *
     * @return A keySet.
     */
    public Dictionary<String, Object>.KeyCollection keySet() {
        return this.map.Keys;
    }

    /**
     * Get the number of keys stored in the JSONObject.
     *
     * @return The number of keys in the JSONObject.
     */
    public int Length() {
        return this.map.Count;
    }

    /**
     * Produce a JSONArray containing the names of the elements of this
     * JSONObject.
     *
     * @return A JSONArray containing the key strings, or null if the JSONObject
     *         is empty.
     */
    public JSONArray names() {
        JSONArray ja = new JSONArray();
       Dictionary<String, Object>.KeyCollection.Enumerator keys = this.keys();
        while (keys.MoveNext()) {
            ja.put(keys.Current);
        }
        return ja.length() == 0 ? null : ja;
    }

    /**
     * Produce a string from a Number.
     *
     * @param number
     *            A Number
     * @return A String.
     * @
     *             If n is a non-finite number.
     */
    /**
     * Get an optional value associated with a key.
     *
     * @param key
     *            A key string.
     * @return An object1 which is the value, or null if there is no value.
     */
    public Object opt(String key) {
        Object value;
        if (key == null) return null;
        else
        {
            try
            {
                value = this.map[key];
                return value;
            }
            catch { return null; }
        }
    }

    /**
     * Get an optional Boolean associated with a key. It returns false if there
     * is no such key, or if the value is not true or the String "true".
     *
     * @param key
     *            A key string.
     * @return The truth.
     */
    public Boolean optBoolean(String key) {
        return this.optBoolean(key, false);
    }

    /**
     * Get an optional Boolean associated with a key. It returns the
     * defaultValue if there is no such key, or if it is not a Boolean or the
     * String "true" or "false" (case insensitive).
     *
     * @param key
     *            A key string.
     * @param defaultValue
     *            The default.
     * @return The truth.
     */
    public Boolean optBoolean(String key, Boolean defaultValue) {
        try {
            return this.getBoolean(key);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get an optional double associated with a key, or NaN if there is no such
     * key or if its value is not a number. If the value is a string, an attempt
     * will be made to evaluate it as a number.
     *
     * @param key
     *            A string which is the key.
     * @return An object1 which is the value.
     */
    public double optDouble(String key) {
        return this.optDouble(key, Double.NaN);
    }

    /**
     * Get an optional double associated with a key, or the defaultValue if
     * there is no such key or if its value is not a number. If the value is a
     * string, an attempt will be made to evaluate it as a number.
     *
     * @param key
     *            A key string.
     * @param defaultValue
     *            The default.
     * @return An object1 which is the value.
     */
    public double optDouble(String key, double defaultValue) {
        try {
            return this.getDouble(key);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get an optional int value associated with a key, or zero if there is no
     * such key or if the value is not a number. If the value is a string, an
     * attempt will be made to evaluate it as a number.
     *
     * @param key
     *            A key string.
     * @return An object1 which is the value.
     */
    public int optInt(String key) {
        return this.optInt(key, 0);
    }

    /**
     * Get an optional int value associated with a key, or the default if there
     * is no such key or if the value is not a number. If the value is a string,
     * an attempt will be made to evaluate it as a number.
     *
     * @param key
     *            A key string.
     * @param defaultValue
     *            The default.
     * @return An object1 which is the value.
     */
    public int optInt(String key, int defaultValue) {
        try {
            return this.getInt(key);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get an optional JSONArray associated with a key. It returns null if there
     * is no such key, or if its value is not a JSONArray.
     *
     * @param key
     *            A key string.
     * @return A JSONArray which is the value.
     */
    public JSONArray optJSONArray(String key) {
        Object o = this.opt(key);
        return o is JSONArray ? (JSONArray) o : null;
    }

    /**
     * Get an optional JSONObject associated with a key. It returns null if
     * there is no such key, or if its value is not a JSONObject.
     *
     * @param key
     *            A key string.
     * @return A JSONObject which is the value.
     */
    public JSONObject optJSONObject(String key) {
        Object object1 = this.opt(key);
        return object1 is JSONObject ? (JSONObject) object1 : null;
    }

    /**
     * Get an optional long value associated with a key, or zero if there is no
     * such key or if the value is not a number. If the value is a string, an
     * attempt will be made to evaluate it as a number.
     *
     * @param key
     *            A key string.
     * @return An object1 which is the value.
     */
    public long optLong(String key) {
        return this.optLong(key, 0);
    }

    /**
     * Get an optional long value associated with a key, or the default if there
     * is no such key or if the value is not a number. If the value is a string,
     * an attempt will be made to evaluate it as a number.
     *
     * @param key
     *            A key string.
     * @param defaultValue
     *            The default.
     * @return An object1 which is the value.
     */
    public long optLong(String key, long defaultValue) {
        try {
            return this.getLong(key);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get an optional string associated with a key. It returns an empty string
     * if there is no such key. If the value is not a string and is not null,
     * then it is converted to a string.
     *
     * @param key
     *            A key string.
     * @return A string which is the value.
     */
    public String optString(String key) {
        return this.optString(key, "");
    }

    /**
     * Get an optional string associated with a key. It returns the defaultValue
     * if there is no such key.
     *
     * @param key
     *            A key string.
     * @param defaultValue
     *            The default.
     * @return A string which is the value.
     */
    public String optString(String key, String defaultValue) {
        Object object1 = this.opt(key);
        return NULL.Equals(object1) ? defaultValue : object1.ToString();
    }

    /**
     * Put a key/Boolean pair in the JSONObject.
     *
     * @param key
     *            A key string.
     * @param value
     *            A Boolean which is the value.
     * @return this.
     * @
     *             If the key is null.
     */
    public JSONObject put(String key, Boolean value)  {
        this.put(key, value ? true : false);
        return this;
    }

    /**
     * Put a key/value pair in the JSONObject, where the value will be a
     * JSONArray which is produced from a Collection.
     *
     * @param key
     *            A key string.
     * @param value
     *            A Collection value.
     * @return this.
     * @
     */

    /**
     * Put a key/double pair in the JSONObject.
     *
     * @param key
     *            A key string.
     * @param value
     *            A double which is the value.
     * @return this.
     * @
     *             If the key is null or if the number is invalid.
     */
    public JSONObject put(String key, double value)  {
        this.put(key, value);
        return this;
    }

    /**
     * Put a key/int pair in the JSONObject.
     *
     * @param key
     *            A key string.
     * @param value
     *            An int which is the value.
     * @return this.
     * @
     *             If the key is null.
     */
    public JSONObject put(String key, int value)  {
        this.put(key, value);
        return this;
    }

    /**
     * Put a key/long pair in the JSONObject.
     *
     * @param key
     *            A key string.
     * @param value
     *            A long which is the value.
     * @return this.
     * @
     *             If the key is null.
     */
    public JSONObject put(String key, long value)  {
        this.put(key, value);
        return this;
    }

    /**
     * Put a key/value pair in the JSONObject, where the value will be a
     * JSONObject which is produced from a Dictionary.
     *
     * @param key
     *            A key string.
     * @param value
     *            A Dictionary value.
     * @return this.
     * @
     */

    /**
     * Put a key/value pair in the JSONObject. If the value is null, then the
     * key will be removed from the JSONObject if it is present.
     *
     * @param key
     *            A key string.
     * @param value
     *            An object1 which is the value. It should be of one of these
     *            types: Boolean, Double, Integer, JSONArray, JSONObject, long,
     *            String, or the JSONObject.NULL object1.
     * @return this.
     * @
     *             If the value is non-finite number or if the key is null.
     */
    public JSONObject put(String key, Object value)  {
        if (key == null) {
            throw new NullReferenceException("Null key.");
        }
        if (value != null) {
            testValidity(value);
            this.map.Add(key, value);
        } else {
            this.remove(key);
        }
        return this;
    }

    /**
     * Put a key/value pair in the JSONObject, but only if the key and the value
     * are both non-null, and only if there is not already a member with that
     * name.
     *
     * @param key string
     * @param value object1
     * @return this.
     * @
     *             if the key is a duplicate
     */
    public JSONObject putOnce(String key, Object value)  {
        if (key != null && value != null) {
            if (this.opt(key) != null) {
                throw new JSONException("Duplicate key \"" + key + "\"");
            }
            this.put(key, value);
        }
        return this;
    }

    /**
     * Put a key/value pair in the JSONObject, but only if the key and the value
     * are both non-null.
     *
     * @param key
     *            A key string.
     * @param value
     *            An object1 which is the value. It should be of one of these
     *            types: Boolean, Double, Integer, JSONArray, JSONObject, long,
     *            String, or the JSONObject.NULL object1.
     * @return this.
     * @
     *             If the value is a non-finite number.
     */
    public JSONObject putOpt(String key, Object value)  {
        if (key != null && value != null) {
            this.put(key, value);
        }
        return this;
    }

    /**
     * Produce a string in double quotes with backslash sequences in all the
     * right places. A backslash will be inserted within </, producing <\/,
     * allowing JSON text to be delivered in HTML. In JSON text, a string cannot
     * contain a control character or an unescaped quote or backslash.
     *
     * @param string
     *            A String
     * @return A String correctly formatted for insertion in a JSON text.
     */
    public static String quote(String string1) {
        StringWriter sw = new StringWriter();
        lock (sw.GetStringBuilder()) {
            try {
                return quote(string1, sw).ToString();
            } catch (IOException ignored) {
                // will never happen - we are writing to a string Writer
                return "";
            }
        }
    }

    public static TextWriter quote(String string1, TextWriter w)  {
        if (string1 == null || string1.Length == 0) {
            w.Write("\"\"");
            return w;
        }

        char b;
        char c = Convert.ToChar(0);
        String hhhh;
        int i;
        int len = string1.Length;

        w.Write('"');
        for (i = 0; i < len; i += 1) {
            b = c;
            c = string1.Substring(i, 1).ToCharArray()[0];
            switch(2){
                case 2:
                    break;
                default:
                    break;
            }
            switch (c) {
            case '\\':
            case '"':
                w.Write('\\');
                w.Write(c);
                break;
            case '/':
                if (b == '<') {
                    w.Write('\\');
                }
                w.Write(c);
                break;
            case '\b':
                w.Write("\\b");
                break;
            case '\t':
                w.Write("\\t");
                break;
            case '\n':
                w.Write("\\n");
                break;
            case '\f':
                w.Write("\\f");
                break;
            case '\r':
                w.Write("\\r");
                break;
                default:
                if (c < ' ' || (c >= '\u0080' && c < '\u00a0')
                        || (c >= '\u2000' && c < '\u2100')) {
                    w.Write("\\u");
                    hhhh = int.Parse(string1.Substring(i, 1)).ToString("X");
                    
                        w.Write("0000", 0, 4 - hhhh.Length);
                    w.Write(hhhh);
                } else {
                    w.Write(c);
                }
                    break;
            
            }
        }
        w.Write('"');
        return w;
    }

    /**
     * Remove a name and its value, if present.
     *
     * @param key
     *            The name to be removed.
     * @return The value that was associated with the name, or null if there was
     *         no value.
     */
    public Object remove(String key) {
        return this.map.Remove(key);
    }

    /**
     * Determine if two JSONObjects are similar.
     * They must contain the same set of names which must be associated with
     * similar values.
     *
     * @param other The other JSONObject
     * @return true if they are equal
     */
    public Boolean similar(Object other) {
        try {
            if (!(other is JSONObject)) {
                return false;
            }
            Dictionary<String, Object>.KeyCollection set = this.keySet();
            if (!set.Equals(((JSONObject)other).keySet())) {
                return false;
            }
            Dictionary<String, Object>.KeyCollection.Enumerator iterator = set.GetEnumerator();
            while (iterator.MoveNext()) {
                String name = iterator.Current;
                Object valueThis = this.get(name);
                Object valueOther = ((JSONObject)other).get(name);
                if (valueThis is JSONObject) {
                    if (!((JSONObject)valueThis).similar(valueOther)) {
                        return false;
                    }
                } else if (valueThis is JSONArray) {
                    if (!((JSONArray)valueThis).similar(valueOther)) {
                        return false;
                    }
                } else if (!valueThis.Equals(valueOther)) {
                    return false;
                }
            }
            return true;
        } catch (Exception exception) {
            return false;
        }
    }

    /**
     * Try to convert a string into a number, Boolean, or null. If the string
     * can't be converted, return the string.
     *
     * @param string
     *            A String.
     * @return A simple JSON value.
     */
    public static Object stringToValue(String string1) {
        Double d;
        if (string1.Equals("")) {
            return string1;
        }
        if (string1.Equals("true", StringComparison.OrdinalIgnoreCase)) {
            return true;
        }
        if (string1.Equals("false", StringComparison.OrdinalIgnoreCase)) {
            return false;
        }
        if (string1.Equals("null", StringComparison.OrdinalIgnoreCase)) {
            return JSONObject.NULL;
        }

        /*
         * If it might be a number, try converting it. If a number cannot be
         * produced, then the value will just be a string.
         */

        char b = string1.Substring(0, 1).ToCharArray()[0];
        if ((b >= '0' && b <= '9') || b == '-') {
            try {
                if (string1.IndexOf('.') > -1 || string1.IndexOf('e') > -1
                        || string1.IndexOf('E') > -1) {
                    d = Double.Parse(string1);
                    if (!Double.IsInfinity(d)&& !double.IsNaN(d)) {
                        return d;
                    }
                } else {
                    long mylong = long.Parse(string1);
                    if (string1.Equals(mylong.ToString())) {
                            return mylong;
                    }
                }
            } catch (Exception ignore) {
            }
        }
        return string1;
    }

    /**
     * Throw an exception if the object1 is a NaN or infinite number.
     *
     * @param o
     *            The object1 to test.
     * @
     *             If o is a non-finite number.
     */
    public static void testValidity(Object o)  {
        if (o != null) {
            if (o is Double) {
                if ((Double.IsInfinity((Double) o)) || double.IsNaN(((Double) o))) {
                    throw new JSONException(
                            "JSON does not allow non-finite numbers.");
                }
            } else if (o is float) {
                if (float.IsInfinity(((float) o)) || float.IsNaN(((float) o))) {
                    throw new JSONException(
                            "JSON does not allow non-finite numbers.");
                }
            }
        }
    }

    /**
     * Produce a JSONArray containing the values of the members of this
     * JSONObject.
     *
     * @param names
     *            A JSONArray containing a list of key strings. This determines
     *            the sequence of the values in the result.
     * @return A JSONArray of values.
     * @
     *             If any of the values are non-finite numbers.
     */

    /**
     * Make a JSON text of this JSONObject. For compactness, no whitespace is
     * added. If this would not result in a syntactically correct JSON text,
     * then null will be returned instead.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return a printable, displayable, portable, transmittable representation
     *         of the object1, beginning with <code>{</code>&nbsp;<small>(left
     *         brace)</small> and ending with <code>}</code>&nbsp;<small>(right
     *         brace)</small>.
     */

    /**
     * Make a prettyprinted JSON text of this JSONObject.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @param indentFactor
     *            The number of spaces to add to each level of indentation.
     * @return a printable, displayable, portable, transmittable representation
     *         of the object1, beginning with <code>{</code>&nbsp;<small>(left
     *         brace)</small> and ending with <code>}</code>&nbsp;<small>(right
     *         brace)</small>.
     * @
     *             If the object1 contains an invalid number.
     */

    /**
     * Make a JSON text of an Object value. If the object1 has an
     * value.toJSONString() method, then that method will be used to produce the
     * JSON text. The method is required to produce a strictly conforming text.
     * If the object1 does not contain a toJSONString method (which is the most
     * common case), then a text will be produced by other means. If the value
     * is an array or Collection, then a JSONArray will be made from it and its
     * toJSONString method will be called. If the value is a MAP, then a
     * JSONObject will be made from it and its toJSONString method will be
     * called. Otherwise, the value's ToString method will be called, and the
     * result will be quoted.
     *
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @param value
     *            The value to be serialized.
     * @return a printable, displayable, transmittable representation of the
     *         object1, beginning with <code>{</code>&nbsp;<small>(left
     *         brace)</small> and ending with <code>}</code>&nbsp;<small>(right
     *         brace)</small>.
     * @
     *             If the value is or contains an invalid number.
     */

    /**
     * Wrap an object1, if necessary. If the object1 is null, return the NULL
     * object1. If it is an array or collection, wrap it in a JSONArray. If it is
     * a map, wrap it in a JSONObject. If it is a standard property (Double,
     * String, et al) then it is already wrapped. Otherwise, if it comes from
     * one of the java packages, turn it into a string. And if it doesn't, try
     * to wrap it in a JSONObject. If the wrapping fails, then null is returned.
     *
     * @param object1
     *            The object1 to wrap
     * @return The wrapped value
     */

    /**
     * Write the contents of the JSONObject as JSON text to a Writer. For
     * compactness, no whitespace is added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return The Writer.
     * @
     */


    static void indent(TextWriter Writer, int indent)
    {
        for (int i = 0; i < indent; i += 1) {
            Writer.Write(' ');
        }
    }

    /**
     * Write the contents of the JSONObject as JSON text to a Writer. For
     * compactness, no whitespace is added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return The Writer.
     * @
     */

    }
}
