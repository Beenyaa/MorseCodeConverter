using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MorseCodeConverter.Models;

namespace MorseCodeConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Dictionary<char, String> morseAlphabet = new Dictionary<char, String>()
            {
                {'A' , ".-"},
                {'B' , "-..."},
                {'C' , "-.-."},
                {'D' , "-.."},
                {'E' , "."},
                {'F' , "..-."},
                {'G' , "--."},
                {'H' , "...."},
                {'I' , ".."},
                {'J' , ".---"},
                {'K' , "-.-"},
                {'L' , ".-.."},
                {'M' , "--"},
                {'N' , "-."},
                {'O' , "---"},
                {'P' , ".--."},
                {'Q' , "--.-"},
                {'R' , ".-."},
                {'S' , "..."},
                {'T' , "-"},
                {'U' , "..-"},
                {'V' , "...-"},
                {'W' , ".--"},
                {'X' , "-..-"},
                {'Y' , "-.--"},
                {'Z' , "--.."},
                {'0' , "-----"},
                {'1' , ".----"},
                {'2' , "..---"},
                {'3' , "...--"},
                {'4' , "....-"},
                {'5' , "....."},
                {'6' , "-...."},
                {'7' , "--..."},
                {'8' , "---.."},
                {'9' , "----."},
                {'&' , ".-..."},
                {'\'' , ".----."},
                {' ', "|" },
            };

        [Route("api/EnglishToMorse/{englishText}")]
        [HttpGet]
        public IActionResult EnglishToMorse(string englishText)
        {
            englishText = englishText.ToUpper();
            string translatedMorse;
            List<String> fullTranslatedMorse = new List<String>();
            string finishedTranslation;
            try
            {
                foreach (char character in englishText)
                {
                    morseAlphabet.TryGetValue(character, out translatedMorse);
                    // do something with entry.Value or entry.Key
                    fullTranslatedMorse.Add(translatedMorse);

                }
                finishedTranslation = string.Join(" ", fullTranslatedMorse);
            }

            catch (Exception)
            {
                return Ok("Something went wrong.");
            }

            var translated = new Translated { TranslatedText = finishedTranslation };
            return Ok(translated);
        }

        [Route("api/MorseToEnglish/{morseText}")]
        [HttpGet]
        public IActionResult MorseToEnglish(string morseText)
        {
            string[] morseTexts = morseText.ToUpper().Split(" ");
            char translatedEnglishChar;
            List<String> fullTranslatedEnglish = new List<String>();
            string finishedTranslation;
            try
            {
                foreach (string morseLetter in morseTexts)
                {
                    translatedEnglishChar = morseAlphabet.FirstOrDefault(x => x.Value == morseLetter).Key;

                    // do something with entry.Value or entry.Key
                    fullTranslatedEnglish.Add(translatedEnglishChar.ToString());

                }
                finishedTranslation = string.Join("", fullTranslatedEnglish);
            }

            catch (Exception)
            {
                return Ok("Something went wrong.");
            }

            var translated = new Translated { TranslatedText = finishedTranslation };
            return Ok(translated);
        }

        public class Translated
        {
            public string TranslatedText { get; set; }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
