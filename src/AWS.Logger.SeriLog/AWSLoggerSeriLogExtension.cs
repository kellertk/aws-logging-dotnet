﻿using System;
using Serilog;
using Serilog.Formatting;
using Serilog.Configuration;
using Microsoft.Extensions.Configuration;

namespace AWS.Logger.SeriLog
{
    /// <summary>
    /// Extensions methods for <see cref="LoggerSinkConfiguration"/> to register <see cref="AWSSink"/>
    /// </summary>
    public static class AWSLoggerSeriLogExtension
    {
        internal const string LOG_GROUP = "Serilog:LogGroup";
        internal const string DONT_CREATE_LOG_GROUP = "Serilog:DontCreateLogGroup";
        internal const string REGION = "Serilog:Region";
        internal const string SERVICEURL = "Serilog:ServiceUrl";
        internal const string PROFILE = "Serilog:Profile";
        internal const string PROFILE_LOCATION = "Serilog:ProfilesLocation";
        internal const string BATCH_PUSH_INTERVAL = "Serilog:BatchPushInterval";
        internal const string BATCH_PUSH_SIZE_IN_BYTES = "Serilog:BatchPushSizeInBytes";
        internal const string MAX_QUEUED_MESSAGES = "Serilog:MaxQueuedMessages";
        internal const string LOG_STREAM_NAME_SUFFIX = "Serilog:LogStreamNameSuffix";
        internal const string LOG_STREAM_NAME_PREFIX = "Serilog:LogStreamNamePrefix";
        internal const string LIBRARY_LOG_FILE_NAME = "Serilog:LibraryLogFileName";

        /// <summary>
        /// AWSSeriLogger target that is called when the customer is using 
        /// Serilog.Settings.Configuration to set the SeriLogger configuration
        /// using a Json input.
        /// </summary>
        public static LoggerConfiguration AWSSeriLog(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IConfiguration configuration, 
                  IFormatProvider iFormatProvider = null,
                  ITextFormatter textFormatter = null )
        {
            AWSLoggerConfig config = new AWSLoggerConfig();

            config.LogGroup = configuration[LOG_GROUP];
            if (configuration[DONT_CREATE_LOG_GROUP] != null)
            {
                config.DontCreateLogGroup = bool.Parse(configuration[DONT_CREATE_LOG_GROUP]);
            }
            if (configuration[REGION] != null)
            {
                config.Region = configuration[REGION];
            }
            if (configuration[SERVICEURL] != null)
            {
                config.ServiceUrl = configuration[SERVICEURL];
            }
            if (configuration[PROFILE] != null)
            {
                config.Profile = configuration[PROFILE];
            }
            if (configuration[PROFILE_LOCATION] != null)
            {
                config.ProfilesLocation = configuration[PROFILE_LOCATION];
            }
            if (configuration[BATCH_PUSH_INTERVAL] != null)
            {
                config.BatchPushInterval = TimeSpan.FromMilliseconds(Int32.Parse(configuration[BATCH_PUSH_INTERVAL]));
            }
            if (configuration[BATCH_PUSH_SIZE_IN_BYTES] != null)
            {
                config.BatchSizeInBytes = Int32.Parse(configuration[BATCH_PUSH_SIZE_IN_BYTES]);
            }
            if (configuration[MAX_QUEUED_MESSAGES] != null)
            {
                config.MaxQueuedMessages = Int32.Parse(configuration[MAX_QUEUED_MESSAGES]);
            }
            if (configuration[LOG_STREAM_NAME_SUFFIX] != null)
            {
                config.LogStreamNameSuffix = configuration[LOG_STREAM_NAME_SUFFIX];
            }
            if (configuration[LOG_STREAM_NAME_PREFIX] != null)
            {
                config.LogStreamNamePrefix = configuration[LOG_STREAM_NAME_PREFIX];
            }
            if (configuration[LIBRARY_LOG_FILE_NAME] != null)
            {
                config.LibraryLogFileName = configuration[LIBRARY_LOG_FILE_NAME];
            }
            return AWSSeriLog(loggerConfiguration, config, iFormatProvider, textFormatter);
        }

        /// <summary>
        /// AWSSeriLogger target that is called when the customer
        /// explicitly creates a configuration of type AWSLoggerConfig 
        /// to set the SeriLogger configuration.
        /// </summary>
        public static LoggerConfiguration AWSSeriLog(
                  this LoggerSinkConfiguration loggerConfiguration,
                   AWSLoggerConfig configuration = null,
                   IFormatProvider iFormatProvider = null, 
                   ITextFormatter textFormatter = null)
        {
            return loggerConfiguration.Sink(new AWSSink(configuration, iFormatProvider, textFormatter));
        }
    }
}
