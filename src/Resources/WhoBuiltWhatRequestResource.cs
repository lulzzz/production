﻿namespace Linn.Production.Resources
{
    public class WhoBuiltWhatRequestResource : FromToDateRequestResource
    {
        public string CitCode { get; set; }

        public int userNumber { get; set; }
    }
}