﻿using StardewModdingAPI;
using System.Collections.Generic;

namespace Omegasis.HappyBirthday
{
    public class PossibleGifts
    {
        private Dictionary<string,string> defaultBirthdayGifts=new Dictionary<string, string>() { 
                ["Universal_Love_Gift"] = "74 1 446 1 204 1 446 5 773 1",
                ["Universal_Like_Gift"] = "-2 3 -7 1 -26 2 -75 5 -80 3 72 1 220 1 221 1 395 1 613 1 634 1 635 1 636 1 637 1 638 1 724 1 233 1 223 1 465 20 -79 5",
                ["Universal_Neutral_Gift"] = "194 1 262 5 -74 5 -75 3 334 5 335 1 390 20 388 20 -81 5 -79 3",
                ["Robin"] = " BestGifts/224 1 426 1 636 1/GoodGift/-6 5 -79 5 424 1 709 1/NeutralGift//",
                ["Demetrius"] = " Best Gifts/207 1 232 1 233 1 400 1/Good Gifts/-5 3 -79 5 422 1/NeutralGift/-4 3/",
                ["Maru"] = " BestGift/72 1 197 1 190 1 215 1 222 1 243 1 336 1 337 1 400 1 787 1/Good Gift/-260 1 62 1 64 1 66 1 68 1 70 1 334 1 335 1 725 1 726 1/NeutralGift/",
                ["Sebastian"] = " Best/84 1 227 1 236 1 575 1 305 1 /Good/267 1 276 1/Neutral/-4 3/",
                ["Linus"] = " Best/88 1 90 1 234 1 242 1 280 1/Good/-5 3 -6 5 -79 5 -81 10/Neutral/-4 3/",
                ["Pierre"] = " Best/202 1/Good/-5 3 -6 5 -7 1 18 1 22 1 402 1 418 1 259 1/Neutral//",
                ["Caroline"] = " Best/213 1 593 1/Good/-7 1 18 1 402 1 418 1/Neutral// ",
                ["Abigail"] = " Best/66 1 128 1 220 1 226 1 276 1 611 1/Good//Neutral// ",
                ["Alex"] = " Best/201 1 212 1 662 1 664 1/Good/-5 3/Neutral// ",
                ["George"] = " Best/20 1 205 1/Good/18 1 195 1 199 1 200 1 214 1 219 1 223 1 231 1 233 1/Neutral// ",
                ["Evelyn"] = " Best/72 1 220 1 239 1 284 1 591 1 595 1/Good/-6 5 18 1 402 1 418 1/Neutral// ",
                ["Lewis"] = " Best/200 1 208 1 235 1 260 1/Good/-80 5 24 1 88 1 90 1 192 1 258 1 264 1 272 1 274 1 278 1/Neutral// ",
                ["Clint"] = " Best/60 1 62 1 64 1 66 1 68 1 70 1 336 1 337 1 605 1 649 1 749 1 337 5/Good/334 20 335 10 336 5/Neutral// ",
                ["Penny"] = " Best/60 1 376 1 651 1 72 1 164 1 218 1 230 1 244 1 254 1/Good/-6 5 20 1 22 1/Neutral// ",
                ["Pam"] = " Best/24 1 90 1 199 1 208 1 303 1 346 1/Good/-6 5 -75 5 -79 5 18 1 227 1 228 1 231 1 232 1 233 1 234 1 235 1 236 1 238 1 402 1 418 1/Neutral/-4 3/ ",
                ["Emily"] = " Best/60 1 62 1 64 1 66 1 68 1 70 1 241 1 428 1 440 1 /Good/18 1 82 1 84 1 86 1 196 1 200 1 207 1 230 1 235 1 402 1 418 1/Neutral// ",
                ["Haley"] = " Best/221 1 421 1 610 1 88 1 /Good/18 1 60 1 62 1 64 1 70 1 88 1 222 1 223 1 232 1 233 1 234 1 402 1 418 1 /Neutral// ",
                ["Jas"] = " Best/221 1 595 1 604 1 /Good/18 1 60 1 64 1 70 1 88 1 232 1 233 1 234 1 222 1 223 1 340 1 344 1 402 1 418 1 /Neutral// ",
                ["Vincent"] = " Best/221 1 398 1 612 1 /Good/18 1 60 1 64 1 70 1 88 1 232 1 233 1 234 1 222 1 223 1 340 1 344 1 402 1 418 1 /Neutral// ",
                ["Jodi"] = " Best/72 1 200 1 211 1 214 1 220 1 222 1 225 1 231 1 /Good/-5 3 -6 5 -79 5 18 1 402 1 418 1 /Neutral// ",
                ["Kent"] = " Best/607 1 649 1 /Good/-5 3 -79 5 18 1 402 1 418 1 /Neutral// ",
                ["Sam"] = " Best/90 1 206 1 655 1 658 1 562 1 731 1/Good/167 1 210 1 213 1 220 1 223 1 224 1 228 1 232 1 233 1 239 1 -5 3/Neutral// ",
                ["Leah"] = " Best/196 1 200 1 348 1 606 1 651 1 650 1 426 1 430 1 /Good/-5 3 -6 5 -79 5 -81 10 18 1 402 1 406 1 408 1 418 1 86 1 /Neutral// ",
                ["Shane"] = " Best/206 1 215 1 260 1 346 1 /Good/-5 3 -79 5 303 1 /Neutral// ",
                ["Marnie"] = " Best/72 1 221 1 240 1 608 1 /Good/-5 3 -6 5 402 1 418 1 /Neutral// ",
                ["Elliott"] = " Best/715 1 732 1 218 1 444 1 /Good/727 1 728 1 -79 5 60 1 80 1 82 1 84 1 149 1 151 1 346 1 348 1 728 1 /Neutral/-4 3 / ",
                ["Gus"] = " Best/72 1 213 1 635 1 729 1 /Good/348 1 303 1 -7 1 18 1 /Neutral// ",
                ["Dwarf"] = " Best/60 1 62 1 64 1 66 1 68 1 70 1 749 1 /Good/82 1 84 1 86 1 96 1 97 1 98 1 99 1 121 1 122 1 /Neutral/-28 20 / ",
                ["Wizard"] = " Best/107 1 155 1 422 1 769 1 768 1 /Good/-12 3 72 1 82 1 84 1/Neutral// ",
                ["Harvey"] = " Best/348 1 237 1 432 1 395 1 342 1 /Good/-81 10 -79 5 -7 1 402 1 418 1 422 1 436 1 438 1 442 1 444 1 422 1 /Neutral// ",
                ["Sandy"] = " Best/18 1 402 1 418 1 /Good/-75 5 -79 5 88 1 428 1 436 1 438 1 440 1 /Neutral// ",
                ["Willy"] = " Best/72 1 143 1 149 1 154 1 276 1 337 1 698 1 /Good/66 1 336 1 340 1 699 1 707 1 /Neutral/-4 3 / ",
                ["Krobus"] = " Best/72 1 16 1 276 1 337 1 305 1 /Good/66 1 336 1 340 1 /Neutral// "
            };

        private Dictionary<string, string> defaultSpouseBirthdayGifts = new Dictionary<string, string>() {

            ["Universal_Love_Gift"] = "74 1 446 1 204 1 446 5 773 1",
            ["Universal_Like_Gift"] = "-2 3 -7 1 -26 2 -75 5 -80 3 72 1 220 1 221 1 395 1 613 1 634 1 635 1 636 1 637 1 638 1 724 1 233 1 223 1 465 20 -79 5",
            ["Universal_Neutral_Gift"] = "194 1 262 5 -74 5 -75 3 334 5 335 1 390 20 388 20 -81 5 -79 3",
            ["Alex"] = "",
            ["Elliott"] = "",
            ["Harvey"] = "",
            ["Sam"] = "",
            ["Sebastian"] = "",
            ["Shane"] = "",
            ["Abigail"] = "",
            ["Emily"] = "",
            ["Haley"] = "",
            ["Leah"] = "",
            ["Maru"] = "",
            ["Penny"] = "",


        };

        public Dictionary<string, string> BirthdayGifts;

        public PossibleGifts()
        {

        }
    }
}
