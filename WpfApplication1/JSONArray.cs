using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace QQWpfApplication1.json
{
    public class JSONArray
    {
        

    /**
     * The arrayList where the JSONArray's properties are kept.
     */
    private ArrayList myArrayList;

    /**
     * Construct an empty JSONArray.
     */
    public JSONArray() {
        this.myArrayList = new ArrayList();
    }

    /**
     * Construct a JSONArray from a JSONTokener.
     *
     * @param x
     *            A JSONTokener
     * @
     *             If there is a syntax error.
     */
    public JSONArray(JSONTokener x)  :this(){
        if (x.nextClean() != '[') {
            throw x.syntaxError("A JSONArray text must start with '['");
        }
        if (x.nextClean() != ']') {
            x.back();
            for (;;) {
                if (x.nextClean() == ',') {
                    x.back();
                    this.myArrayList.Add(JSONObject.NULL);
                } else {
                    x.back();
                    this.myArrayList.Add(x.nextValue());
                }
                switch (x.nextClean()) {
                case ',':
                    if (x.nextClean() == ']') {
                        return;
                    }
                    x.back();
                    break;
                case ']':
                    return;
                default:
                    throw x.syntaxError("Expected a ',' or ']'");
                }
            }
        }
    }

    /**
     * Construct a JSONArray from a source JSON text.
     *
     * @param source
     *            A string that begins with <code>[</code>&nbsp;<small>(left
     *            bracket)</small> and ends with <code>]</code>
     *            &nbsp;<small>(right bracket)</small>.
     * @
     *             If there is a syntax error.
     */

    /**
     * Construct a JSONArray from a Collection.
     *
     * @param collection
     *            A Collection.
     */

    /**
     * Construct a JSONArray from an array
     *
     * @
     *             If not an array.
     */

    /**
     * Get the object1 value associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return An object1 value.
     * @
     *             If there is no value for the index.
     */
    public Object get(int index)  {
        Object object1 = this.opt(index);
        if (object1 == null) {
            throw new JSONException("JSONArray[" + index + "] not found.");
        }
        return object1;
    }

    /**
     * Get the Boolean value associated with an index. The string values "true"
     * and "false" are converted to Boolean.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The truth.
     * @
     *             If there is no value for the index or if the value is not
     *             convertible to Boolean.
     */
    public Boolean getBoolean(int index)  {
        Object object1 = this.get(index);
        if (object1.Equals(false)
                || (object1 is String && ((String) object1)
                        .Equals("false", StringComparison.OrdinalIgnoreCase))) {
            return false;
        } else if (object1.Equals(true)
                || (object1 is String && ((String) object1)
                        .Equals("true", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        throw new JSONException("JSONArray[" + index + "] is not a Boolean.");
    }

    /**
     * Get the double value associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     * @
     *             If the key is not found or if the value cannot be converted
     *             to a number.
     */
    public double getDouble(int index)  {
        String object1 = this.get(index)+"";
        try {
                   return Double.Parse((String) object1);
        } catch (Exception e) {
            throw new JSONException("JSONArray[" + index + "] is not a number.");
        }
    }

    /**
     * Get the int value associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     * @
     *             If the key is not found or if the value is not a number.
     */
    public int getInt(int index)  {
        String object1 = this.get(index)+"";
        try {
                    return  int.Parse((String) object1);
        } catch (Exception e) {
            throw new JSONException("JSONArray[" + index + "] is not a number.");
        }
    }

    /**
     * Get the JSONArray associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return A JSONArray value.
     * @
     *             If there is no value for the index. or if the value is not a
     *             JSONArray
     */
    public JSONArray getJSONArray(int index)  {
        Object object1 = this.get(index);
        if (object1 is JSONArray) {
            return (JSONArray) object1;
        }
        throw new JSONException("JSONArray[" + index + "] is not a JSONArray.");
    }

    /**
     * Get the JSONObject associated with an index.
     *
     * @param index
     *            subscript
     * @return A JSONObject value.
     * @
     *             If there is no value for the index or if the value is not a
     *             JSONObject
     */
    public JSONObject getJSONObject(int index)  {
        Object object1 = this.get(index);
        if (object1 is JSONObject) {
            return (JSONObject) object1;
        }
        throw new JSONException("JSONArray[" + index + "] is not a JSONObject.");
    }

    /**
     * Get the long value associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     * @
     *             If the key is not found or if the value cannot be converted
     *             to a number.
     */
    public long getLong(int index)  {
        String object1 = this.get(index)+"";
        try {
                   return long.Parse((String)object1);
        } catch (Exception e) {
            throw new JSONException("JSONArray[" + index + "] is not a number.");
        }
    }

    /**
     * Get the string associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return A string value.
     * @
     *             If there is no string value for the index.
     */
    public String getString(int index)  {
        Object object1 = this.get(index);
        if (object1 is String) {
            return (String) object1;
        }
        throw new JSONException("JSONArray[" + index + "] not a string.");
    }

    /**
     * Determine if the value is null.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return true if the value at the index is null, or if there is no value.
     */
    public Boolean isNull(int index) {
        return JSONObject.NULL.Equals(this.opt(index));
    }

    /**
     * Make a string from the contents of this JSONArray. The
     * <code>separator</code> string is inserted between each element. Warning:
     * This method assumes that the data structure is acyclical.
     *
     * @param separator
     *            A string that will be inserted between the elements.
     * @return a string.
     * @
     *             If the array contains an invalid number.
     */

    /**
     * Get the number of elements in the JSONArray, included nulls.
     *
     * @return The length (or Count).
     */
    public int length() {
        return this.myArrayList.Count;
    }

    /**
     * Get the optional object1 value associated with an index.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return An object1 value, or null if there is no object1 at that index.
     */
    public Object opt(int index) {
        return (index < 0 || index >= this.length()) ? null : this.myArrayList[index];
               
    }

    /**
     * Get the optional Boolean value associated with an index. It returns false
     * if there is no value at that index, or if the value is not true
     * or the String "true".
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The truth.
     */
    public Boolean optBoolean(int index) {
        return this.optBoolean(index, false);
    }

    /**
     * Get the optional Boolean value associated with an index. It returns the
     * defaultValue if there is no value at that index or if it is not a Boolean
     * or the String "true" or "false" (case insensitive).
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @param defaultValue
     *            A Boolean default.
     * @return The truth.
     */
    public Boolean optBoolean(int index, Boolean defaultValue) {
        try {
            return this.getBoolean(index);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get the optional double value associated with an index. NaN is returned
     * if there is no value for the index, or if the value is not a number and
     * cannot be converted to a number.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     */
    public double optDouble(int index) {
        return this.optDouble(index, Double.NaN);
    }

    /**
     * Get the optional double value associated with an index. The defaultValue
     * is returned if there is no value for the index, or if the value is not a
     * number and cannot be converted to a number.
     *
     * @param index
     *            subscript
     * @param defaultValue
     *            The default value.
     * @return The value.
     */
    public double optDouble(int index, double defaultValue) {
        try {
            return this.getDouble(index);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get the optional int value associated with an index. Zero is returned if
     * there is no value for the index, or if the value is not a number and
     * cannot be converted to a number.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     */
    public int optInt(int index) {
        return this.optInt(index, 0);
    }

    /**
     * Get the optional int value associated with an index. The defaultValue is
     * returned if there is no value for the index, or if the value is not a
     * number and cannot be converted to a number.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @param defaultValue
     *            The default value.
     * @return The value.
     */
    public int optInt(int index, int defaultValue) {
        try {
            return this.getInt(index);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get the optional JSONArray associated with an index.
     *
     * @param index
     *            subscript
     * @return A JSONArray value, or null if the index has no value, or if the
     *         value is not a JSONArray.
     */
    public JSONArray optJSONArray(int index) {
        Object o = this.opt(index);
        return o is JSONArray ? (JSONArray) o : null;
    }

    /**
     * Get the optional JSONObject associated with an index. Null is returned if
     * the key is not found, or null if the index has no value, or if the value
     * is not a JSONObject.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return A JSONObject value.
     */
    public JSONObject optJSONObject(int index) {
        Object o = this.opt(index);
        return o is JSONObject ? (JSONObject) o : null;
    }

    /**
     * Get the optional long value associated with an index. Zero is returned if
     * there is no value for the index, or if the value is not a number and
     * cannot be converted to a number.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return The value.
     */
    public long optLong(int index) {
        return this.optLong(index, 0);
    }

    /**
     * Get the optional long value associated with an index. The defaultValue is
     * returned if there is no value for the index, or if the value is not a
     * number and cannot be converted to a number.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @param defaultValue
     *            The default value.
     * @return The value.
     */
    public long optLong(int index, long defaultValue) {
        try {
            return this.getLong(index);
        } catch (Exception e) {
            return defaultValue;
        }
    }

    /**
     * Get the optional string value associated with an index. It returns an
     * empty string if there is no value at that index. If the value is not a
     * string and is not null, then it is coverted to a string.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @return A String value.
     */
    public String optString(int index) {
        return this.optString(index, "");
    }

    /**
     * Get the optional string associated with an index. The defaultValue is
     * returned if the key is not found.
     *
     * @param index
     *            The index must be between 0 and length() - 1.
     * @param defaultValue
     *            The default value.
     * @return A String value.
     */
    public String optString(int index, String defaultValue) {
        Object object1 = this.opt(index);
        return JSONObject.NULL.Equals(object1) ? defaultValue : object1
                .ToString();
    }

    /**
     * Append a Boolean value. This increases the array's length by one.
     *
     * @param value
     *            A Boolean value.
     * @return this.
     */
    public JSONArray put(Boolean value) {
        this.put(value ? true : false);
        return this;
    }

    /**
     * Put a value in the JSONArray, where the value will be a JSONArray which
     * is produced from a Collection.
     *
     * @param value
     *            A Collection value.
     * @return this.
     */

    /**
     * Append a double value. This increases the array's length by one.
     *
     * @param value
     *            A double value.
     * @
     *             if the value is not finite.
     * @return this.
     */

    /**
     * Append an int value. This increases the array's length by one.
     *
     * @param value
     *            An int value.
     * @return this.
     */

    /**
     * Append an long value. This increases the array's length by one.
     *
     * @param value
     *            A long value.
     * @return this.
     */

    /**
     * Put a value in the JSONArray, where the value will be a JSONObject which
     * is produced from a Map.
     *
     * @param value
     *            A Map value.
     * @return this.
     */

    /**
     * Append an object1 value. This increases the array's length by one.
     *
     * @param value
     *            An object1 value. The value should be a Boolean, Double,
     *            Integer, JSONArray, JSONObject, Long, or String, or the
     *            JSONObject.NULL object1.
     * @return this.
     */
    public JSONArray put(Object value) {
        this.myArrayList.Add(value);
        return this;
    }

    /**
     * Put or replace a Boolean value in the JSONArray. If the index is greater
     * than the length of the JSONArray, then null elements will be Added as
     * necessary to pad it out.
     *
     * @param index
     *            The subscript.
     * @param value
     *            A Boolean value.
     * @return this.
     * @
     *             If the index is negative.
     */
    public JSONArray put(int index, Boolean value)  {
        this.put(index, value ? true : false);
        return this;
    }

    /**
     * Put a value in the JSONArray, where the value will be a JSONArray which
     * is produced from a Collection.
     *
     * @param index
     *            The subscript.
     * @param value
     *            A Collection value.
     * @return this.
     * @
     *             If the index is negative or if the value is not finite.
     */

    /**
     * Put or replace a double value. If the index is greater than the length of
     * the JSONArray, then null elements will be Added as necessary to pad it
     * out.
     *
     * @param index
     *            The subscript.
     * @param value
     *            A double value.
     * @return this.
     * @
     *             If the index is negative or if the value is not finite.
     */

    /**
     * Put or replace an int value. If the index is greater than the length of
     * the JSONArray, then null elements will be Added as necessary to pad it
     * out.
     *
     * @param index
     *            The subscript.
     * @param value
     *            An int value.
     * @return this.
     * @
     *             If the index is negative.
     */
    public JSONArray put(int index, int value)  {
        this.put(index, value);
        return this;
    }

    /**
     * Put or replace a long value. If the index is greater than the length of
     * the JSONArray, then null elements will be Added as necessary to pad it
     * out.
     *
     * @param index
     *            The subscript.
     * @param value
     *            A long value.
     * @return this.
     * @
     *             If the index is negative.
     */
    public JSONArray put(int index, long value)  {
        this.put(index, value);
        return this;
    }

    /**
     * Put a value in the JSONArray, where the value will be a JSONObject that
     * is produced from a Map.
     *
     * @param index
     *            The subscript.
     * @param value
     *            The Map value.
     * @return this.
     * @
     *             If the index is negative or if the the value is an invalid
     *             number.
     */

    /**
     * Put or replace an object1 value in the JSONArray. If the index is greater
     * than the length of the JSONArray, then null elements will be Added as
     * necessary to pad it out.
     *
     * @param index
     *            The subscript.
     * @param value
     *            The value to put into the array. The value should be a
     *            Boolean, Double, Integer, JSONArray, JSONObject, Long, or
     *            String, or the JSONObject.NULL object1.
     * @return this.
     * @
     *             If the index is negative or if the the value is an invalid
     *             number.
     */
    public JSONArray put(int index, Object value)  {
        JSONObject.testValidity(value);
        if (index < 0) {
            throw new JSONException("JSONArray[" + index + "] not found.");
        }
        if (index < this.length()) {
            this.myArrayList.ToArray()[index]=  value;
        } else {
            while (index != this.length()) {
                this.put(JSONObject.NULL);
            }
            this.put(value);
        }
        return this;
    }

    /**
     * Remove an index and close the hole.
     *
     * @param index
     *            The index of the element to be removed.
     * @return The value that was associated with the index, or null if there
     *         was no value.
     */
    public Object remove(int index) {
        if (index >= 0 && index < this.length())
        {
            this.myArrayList.Remove(index);
            return this.myArrayList;
        }
        else
        {
            return null;
        }
    }

    /**
     * Determine if two JSONArrays are similar.
     * They must contain similar sequences.
     *
     * @param other The other JSONArray
     * @return true if they are equal
     */
    public Boolean similar(Object other) {
        if (!(other is JSONArray)) {
            return false;
        }
        int len = this.length();
        if (len != ((JSONArray)other).length()) {
            return false;
        }
        for (int i = 0; i < len; i += 1) {
            Object valueThis = this.get(i);
            Object valueOther = ((JSONArray)other).get(i);
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
    }

    /**
     * Produce a JSONObject by combining a JSONArray of names with the values of
     * this JSONArray.
     *
     * @param names
     *            A JSONArray containing a list of key strings. These will be
     *            paired with the values.
     * @return A JSONObject, or null if there are no names or if this JSONArray
     *         has no values.
     * @
     *             If any of the names are null.
     */

    /**
     * Make a JSON text of this JSONArray. For compactness, no unnecessary
     * whitespace is Added. If it is not possible to produce a syntactically
     * correct JSON text then null will be returned instead. This could occur if
     * the array contains an invalid number.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return a printable, displayable, transmittable representation of the
     *         array.
     */

    /**
     * Make a prettyprinted JSON text of this JSONArray. Warning: This method
     * assumes that the data structure is acyclical.
     *
     * @param indentFactor
     *            The number of spaces to Add to each level of indentation.
     * @return a printable, displayable, transmittable representation of the
     *         object1, beginning with <code>[</code>&nbsp;<small>(left
     *         bracket)</small> and ending with <code>]</code>
     *         &nbsp;<small>(right bracket)</small>.
     * @
     */

    /**
     * Write the contents of the JSONArray as JSON text to a Writer. For
     * compactness, no whitespace is Added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return The Writer.
     * @
     */

    /**
     * Write the contents of the JSONArray as JSON text to a Writer. For
     * compactness, no whitespace is Added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @param indentFactor
     *            The number of spaces to Add to each level of indentation.
     * @param indent
     *            The indention of the top level.
     * @return The Writer.
     * @
     */
    public TextWriter Write(TextWriter writer)
    {
             return this.Write(writer, 0, 0);
    }

    /**
     * Write the contents of the JSONArray as JSON text to a writer. For
     * compactness, no whitespace is added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @param indentFactor
     *            The number of spaces to add to each level of indentation.
     * @param indent
     *            The indention of the top level.
     * @return The writer.
     * @throws JSONException
     */
    public TextWriter Write(TextWriter Writer, int indentFactor, int indent) {
        try {
            Boolean commanate = false;
            int length = this.length();
            Writer.Write('[');

            if (length == 1) {
                JSONObject.WriteValue(Writer, this.myArrayList[0],
                        indentFactor, indent);
            } else if (length != 0) {
                int newindent = indent + indentFactor;

                for (int i = 0; i < length; i += 1) {
                    if (commanate) {
                        Writer.Write(',');
                    }
                    if (indentFactor > 0) {
                        Writer.Write('\n');
                    }
                    JSONObject.indent(Writer, newindent);
                    JSONObject.WriteValue(Writer, this.myArrayList[i],
                            indentFactor, newindent);
                    commanate = true;
                }
                if (indentFactor > 0) {
                    Writer.Write('\n');
                }
                JSONObject.indent(Writer, indent);
            }
            Writer.Write(']');
            return Writer;
        } catch (IOException e) {
            throw new JSONException(e);
        }
    }
    }
}
