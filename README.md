# ReformatJson
This is a business logic module designed to reformat a JSON string into something easier for a human to read. As a business logic module, it needs to be called from a user interface (be that console, Web or Windows), which will supply the string, receive and display the reformatted one and allows for the specification of the deliminators and separateors.

To use this module, you will need to initialise an instance of ReformatJson,then call the MakeReadable method. This method accepts 1-3 parameters, all strings. The first (required) parameter is the JSON to reformat. The second (optional) parameter is the character(s) to use as an indent. If not specified, this will be 4 spaces, peerindent. You can over-ride it, if you want to use 2 spaces, or a tab or some other string. Similarly, the third (optional) parameter is the character(s) to use to separate tthe value of a property from its name. If not set, this will default to a colon (:), but you may prefer a colon and a space (: ), or some other character or string. 

## Example of Use
ResformatJson testReader = new();

string  resultJson = (string)testReader.MakeReadable(inputJson, "    ", ":");

## Example of Results
With an initial JSON of {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

Run with the default settings, this will produce the following output:

{

    "id":"Bibble",
    
    "name":"Test Name",
    
    "parcels":1,
    
    "value":11.50,
    
    "items":[
    
        {
        
            "name":"Box 17"
            
        },
        
        {
        
            "name":"22 Sphere"
            
        },
        
        {
        
            "name":"Alphabet"
            
        }
        
    ],
    
    "saturdayDelivery":true
    
}
