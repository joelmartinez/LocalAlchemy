**usage**

 1. Get a Bing Search API app key: http://www.bing.com/toolbox/bingdeveloper/
 2. Create a file named '.localalchemy' in your user directory (c:\users\_username_\.localalchemy) which contains that key and nothing else
 3. Use the command line tool: `localalchemy.exe -s localizable.strings -d es`. Where "es" in this example is the language code. You can find the list of supported languages/codes here: http://msdn.microsoft.com/en-us/library/dd250941.aspx

The tool will translate each string element, and output a file named localizable.es.strings