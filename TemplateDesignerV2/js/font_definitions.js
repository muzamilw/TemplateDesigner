

(function(){
  var fontDefinitions = {
      'Calibri': 85,
      'Myriad Web Pro': 100,
      'Trebuchet MS': 10,
      'Arial': 90,
      'Brush Script MT': 110,
      'Comic Sans MS': 180,
      'Impact': 100,
    'CrashCTT_400':                       60,
    'CA_BND_Web_Bold_700':                60,
    'Delicious_500':                      80,
    'Tallys_400':                         70,
    'Arial Narrow': 130
  };
  for (var prop in fontDefinitions) {
    if (Cufon.fonts[prop.toLowerCase()]) {
      Cufon.fonts[prop.toLowerCase()].offsetLeft = fontDefinitions[prop];
    }
  }
})();



