﻿using System;

namespace Core.Helpers
{
    public static class TimeConversionExtensions
    {
        public static TimeSpan Ticks(this int value) => TimeSpan.FromTicks((long) value);

        public static TimeSpan Ticks(this long value) => TimeSpan.FromTicks(value);

        public static TimeSpan Milliseconds(this ushort value) => TimeSpan.FromMilliseconds((double) value);

        public static TimeSpan Milliseconds(this int value) => TimeSpan.FromMilliseconds((double) value);

        public static TimeSpan Milliseconds(this long value) => TimeSpan.FromMilliseconds((double) value);

        public static TimeSpan Milliseconds(this double value) => TimeSpan.FromMilliseconds(value);

        public static TimeSpan Seconds(this ushort value) => TimeSpan.FromSeconds((double) value);

        public static TimeSpan Seconds(this int value) => TimeSpan.FromSeconds((double) value);

        public static TimeSpan Seconds(this long value) => TimeSpan.FromSeconds((double) value);

        public static TimeSpan Seconds(this double value) => TimeSpan.FromSeconds(value);

        public static TimeSpan Minutes(this ushort value) => TimeSpan.FromMinutes((double) value);

        public static TimeSpan Minutes(this int value) => TimeSpan.FromMinutes((double) value);

        public static TimeSpan Minutes(this long value) => TimeSpan.FromMinutes((double) value);

        public static TimeSpan Minutes(this double value) => TimeSpan.FromMinutes(value);

        public static TimeSpan Hours(this ushort value) => TimeSpan.FromHours((double) value);

        public static TimeSpan Hours(this int value) => TimeSpan.FromHours((double) value);

        public static TimeSpan Hours(this long value) => TimeSpan.FromHours((double) value);

        public static TimeSpan Hours(this double value) => TimeSpan.FromHours(value);

        public static TimeSpan Days(this ushort value) => TimeSpan.FromDays((double) value);

        public static TimeSpan Days(this int value) => TimeSpan.FromDays((double) value);

        public static TimeSpan Days(this long value) => TimeSpan.FromDays((double) value);

        public static TimeSpan Days(this double value) => TimeSpan.FromDays(value);
    }
}