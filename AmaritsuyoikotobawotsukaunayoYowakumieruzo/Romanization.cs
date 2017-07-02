using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo {
    static class Romanization {
        public static ReadOnlyDictionary<string, string> Roma { get; } =
            new ReadOnlyDictionary<string, string>(new Dictionary<string, string> {
                //あ行
                {"あ", "a"},
                {"い", "i"},
                {"う", "u"},
                {"え", "e"},
                {"お", "o"},
                //か行
                {"か", "ka"},
                {"き", "ki"},
                {"く", "ku"},
                {"け", "ke"},
                {"こ", "ko"},
                //きゃ
                {"きゃ", "kya"},
                {"きゅ", "kyu"},
                {"きょ", "kyo"},
                //が行
                {"が", "ga"},
                {"ぎ", "gi"},
                {"ぐ", "gu"},
                {"げ", "ge"},
                {"ご", "go"},
                //ぎゃ
                {"ぎゃ", "gya"},
                {"ぎゅ", "gyu"},
                {"ぎょ", "gyo"},
                //さ行
                {"さ", "sa"},
                {"し", "si"},
                {"す", "su"},
                {"せ", "se"},
                {"そ", "so"},
                //しゃ
                {"しゃ", "sya"},
                {"しゅ", "syu"},
                {"しょ", "syo"},
                //ざ行
                {"ざ", "za"},
                {"じ", "zi"},
                {"ず", "zu"},
                {"ぜ", "ze"},
                {"ぞ", "zo"},
                //じゃ
                {"じゃ", "zya"},
                {"じゅ", "zyu"},
                {"じょ", "zyo"},
                //た行
                {"た", "ta"},
                {"ち", "ti"},
                {"つ", "tu"},
                {"て", "te"},
                {"と", "to"},
                //ちゃ
                {"ちゃ", "tya"},
                {"ちゅ", "tyu"},
                {"ちょ", "tyo"},
                //だ行
                {"だ", "da"},
                {"ぢ", "zi"},
                {"づ", "zu"},
                {"で", "de"},
                {"ど", "do"},
                //ぢゃ
                {"ぢゃ", "zya"},
                {"ぢゅ", "zyu"},
                {"ぢょ", "zyo"},
                //な行
                {"な", "na"},
                {"に", "ni"},
                {"ぬ", "nu"},
                {"ね", "ne"},
                {"の", "no"},
                //にゃ
                {"にゃ", "nya"},
                {"にゅ", "nyu"},
                {"にょ", "nyo"},
                //は行
                {"は", "ha"},
                {"ひ", "hi"},
                {"ふ", "hu"},
                {"へ", "he"},
                {"ほ", "ho"},
                //ひゃ
                {"ひゃ", "hya"},
                {"ひゅ", "hyu"},
                {"ひょ", "hyo"},
                //ば行
                {"ば", "ba"},
                {"び", "bi"},
                {"ぶ", "bu"},
                {"べ", "be"},
                {"ぼ", "bo"},
                //びゃ
                {"びゃ", "bya"},
                {"びゅ", "byu"},
                {"びょ", "byo"},
                //ぱ行
                {"ぱ", "pa"},
                {"ぴ", "pi"},
                {"ぷ", "pu"},
                {"ぺ", "pe"},
                {"ぽ", "po"},
                //ぴゃ
                {"ぴゃ", "pya"},
                {"ぴゅ", "pyu"},
                {"ぴょ", "pyo"},
                //ま行
                {"ま", "ma"},
                {"み", "mi"},
                {"む", "mu"},
                {"め", "me"},
                {"も", "mo"},
                //みゃ
                {"みゃ", "mya"},
                {"みゅ", "myu"},
                {"いょ", "myo"},
                //や行
                {"や", "ya"},
                {"ゆ", "yu"},
                {"よ", "yo"},
                //ら行
                {"ら", "ra"},
                {"り", "ri"},
                {"る", "ru"},
                {"れ", "re"},
                {"ろ", "ro"},
                //わ行
                {"わ", "wa"},
                {"を", "o"},
                {"ん", "n"}
            });
    }
}