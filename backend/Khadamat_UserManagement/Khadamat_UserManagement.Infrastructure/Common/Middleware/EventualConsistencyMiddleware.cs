//using Khadamat_UserManagement.Application.Common.Interfaces;
//using Khadamat_UserManagement.Application.Common.Models;
//using Khadamat_UserManagement.Domain.Common.Events;
//using Khadamat_UserManagement.Domain.UserAggregate.Events;
//using Khadamat_UserManagement.Infrastructure.Common.Persistence;
//using Khadamat_UserManagement.Infrastructure.Logging;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Khadamat_UserManagement.Infrastructure.Common.Middleware
//{
//    public class EventualConsistencyMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<EventualConsistencyMiddleware> _logger;

//        public EventualConsistencyMiddleware(
//            RequestDelegate next,
//            ILogger<EventualConsistencyMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(
//                    HttpContext context,
//                    IPublisher publisher,
//                    Khadamat_UserDbContext dbContext,
//                    IEmailSender emailSender,
//                    ICurrentUserProvider userProvider,
//                    LogsCollector logsCollector)
//        {
//            var currentUser = GetCurrentUserSafe(context, userProvider);

//            {
//                var notes = currentUser.Id == 0 ? "UserId = 0 because this request is being made by an unauthenticated user, like Login or Register requests" : "No Notes";

//                logsCollector.AddLog(GetLog(
//                    UserId: currentUser.Id,
//                    EventType: null,
//                    EventBody: null,
//                    RequestId: context.TraceIdentifier,
//                    RequestPath: context.Request.Path,
//                    EventCriticality: null,
//                    Notes: notes,
//                    Message: $"Starting request processing for user {currentUser.Id}",
//                    TimeStamp: DateTime.Now,
//                    Level: "Information",
//                    Exception: null));

//                var transaction = await dbContext.Database.BeginTransactionAsync();

//                context.Response.OnCompleted(async () =>
//                {
//                    try
//                    {
//                        if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
//                            value is Queue<IDomainEvent> domainEvents)
//                        {
//                            logsCollector.AddLog(GetLog(
//                                UserId: currentUser.Id,
//                                EventType: null,
//                                EventBody: null,
//                                RequestId: context.TraceIdentifier,
//                                RequestPath: context.Request.Path,
//                                EventCriticality: null,
//                                Notes: notes,
//                                Message: $"Found {domainEvents.Count} domain events to process",
//                                TimeStamp: DateTime.Now,
//                                Level: "Information",
//                                Exception: null));

//                            // Process critical events with error logging
//                            foreach (var evt in domainEvents.OfType<ICriticalDomainEvent>())
//                            {
//                                string evtBody = JsonSerializer.Serialize(evt, evt.GetType());

//                                {
//                                    try
//                                    {
//                                        await publisher.Publish(evt);

//                                        logsCollector.AddLog(GetLog(
//                                            UserId: currentUser.Id,
//                                            EventType: evt.GetType().Name,
//                                            EventBody: evtBody,
//                                            RequestId: context.TraceIdentifier,
//                                            RequestPath: context.Request.Path,
//                                            EventCriticality: "CRITICAL",
//                                            Notes: null,
//                                            Message: $"Processed critical event {evt.GetType().Name}",
//                                            TimeStamp: DateTime.Now,
//                                            Level: "Information",
//                                            Exception: null));


//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        //_logger.LogCritical(ex, "CRITICAL FAILURE processing {EventType}", evt.GetType().Name);

//                                        logsCollector.AddLog(GetLog(
//                                            UserId: currentUser.Id,
//                                            EventType: evt.GetType().Name,
//                                            EventBody: evtBody,
//                                            RequestId: context.TraceIdentifier,
//                                            RequestPath: context.Request.Path,
//                                            EventCriticality: "CRITICAL",
//                                            Notes: null,
//                                            Message: $"CRITICAL FAILURE processing {evt.GetType().Name}",
//                                            TimeStamp: DateTime.Now,
//                                            Level: "Critical",
//                                            Exception: ex.Message));

//                                        throw;
//                                    }
//                                }
//                            }

//                            await transaction.CommitAsync();
//                            //_logger.LogInformation("Transaction committed");

//                            logsCollector.AddLog(GetLog(
//                                            UserId: currentUser.Id,
//                                            EventType: null,
//                                            EventBody: null,
//                                            RequestId: context.TraceIdentifier,
//                                            RequestPath: context.Request.Path,
//                                            EventCriticality: null,
//                                            Notes: null,
//                                            Message: "Transaction committed",
//                                            TimeStamp: DateTime.Now,
//                                            Level: "Information",
//                                            Exception: null));


//                            // Process non-critical events with warning logging
//                            foreach (var evt in domainEvents.OfType<INonCriticalDomainEvent>())
//                            {
//                                string evtBody = JsonSerializer.Serialize(evt, evt.GetType());
//                                //using (_logger.BeginScope(new Dictionary<string, object>
//                                //{
//                                //    ["EventType"] = evt.GetType().Name,
//                                //    ["EventCriticality"] = "NonCritical",
//                                //    ["EventBody"] = evtBody
//                                //}))
//                                {
//                                    try
//                                    {
//                                        await publisher.Publish(evt);
//                                        //_logger.LogInformation("Processed non-critical event {EventType}", evt.GetType().Name);


//                                        logsCollector.AddLog(GetLog(
//                                            UserId: currentUser.Id,
//                                            EventType: evt.GetType().Name,
//                                            EventBody: evtBody,
//                                            RequestId: context.TraceIdentifier,
//                                            RequestPath: context.Request.Path,
//                                            EventCriticality: "NON-CRITICAL",
//                                            Notes: null,
//                                            Message: $"Processed non-critical event {evt.GetType().Name}",
//                                            TimeStamp: DateTime.Now,
//                                            Level: "Information",
//                                            Exception: null));

//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        //_logger.LogWarning(ex, "Non-critical event {EventType} failed", evt.GetType().Name);

//                                        logsCollector.AddLog(GetLog(
//                                            UserId: currentUser.Id,
//                                            EventType: evt.GetType().Name,
//                                            EventBody: evtBody,
//                                            RequestId: context.TraceIdentifier,
//                                            RequestPath: context.Request.Path,
//                                            EventCriticality: "NON-CRITICAL",
//                                            Notes: null,
//                                            Message: $"Non-critical event {evt.GetType().Name} failed",
//                                            TimeStamp: DateTime.Now,
//                                            Level: "Warning",
//                                            Exception: ex.Message));

//                                        await HandleNonCriticalError(ex, emailSender, evt, currentUser, logsCollector, context);
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            await transaction.CommitAsync();
//                            //_logger.LogInformation("No domain events to process");

//                            logsCollector.AddLog(GetLog(
//                                UserId: currentUser.Id,
//                                EventType: null,
//                                EventBody: null,
//                                RequestId: context.TraceIdentifier,
//                                RequestPath: context.Request.Path,
//                                EventCriticality: null,
//                                Notes: null,
//                                Message: $"No domain events to process",
//                                TimeStamp: DateTime.Now,
//                                Level: "Information",
//                                Exception: null));

//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        //_logger.LogCritical(ex, "CATASTROPHIC FAILURE in event processing - rolled back");

//                        logsCollector.AddLog(GetLog(
//                            UserId: currentUser.Id,
//                            EventType: null,
//                            EventBody: null,
//                            RequestId: context.TraceIdentifier,
//                            RequestPath: context.Request.Path,
//                            EventCriticality: null,
//                            Notes: null,
//                            Message: "CATASTROPHIC FAILURE in event processing - rolled back",
//                            TimeStamp: DateTime.Now,
//                            Level: "Critical",
//                            Exception: ex.Message));

//                        await transaction.RollbackAsync();
//                        await HandleCriticalError(ex, emailSender, currentUser, logsCollector, context);
//                    }
//                    finally
//                    {
//                        await transaction.DisposeAsync();
//                        await logsCollector.CommitLogs();
//                    }
//                });

//                await _next(context);
//            }
//        }



//        private async Task HandleCriticalError(Exception ex, IEmailSender emailSender, CurrentUser user, LogsCollector logsCollector, HttpContext context)
//        {
//            //_logger.LogCritical(ex, "Critical error occurred during event processing");

//            logsCollector.AddLog(GetLog(
//                UserId: user.Id,
//                EventType: null,
//                EventBody: null,
//                RequestId: context.TraceIdentifier,
//                RequestPath: context.Request.Path,
//                EventCriticality: null,
//                Notes: null,
//                Message: $"Critical error occurred during event processing",
//                TimeStamp: DateTime.Now,
//                Level: "Critical",
//                Exception: ex.Message));


//            try
//            {
//                await emailSender.SendEmailAsync(
//                    "layth.work.syriatel@gmail.com",
//                    user.Email,
//                    "Critical System Error",
//                    $"An important operation failed: {ex.Message}");

//                //_logger.LogInformation("Sent critical error notification to {UserEmail}", user.Email);

//                logsCollector.AddLog(GetLog(
//                    UserId: user.Id,
//                    EventType: null,
//                    EventBody: null,
//                    RequestId: context.TraceIdentifier,
//                    RequestPath: context.Request.Path,
//                    EventCriticality: null,
//                    Notes: null,
//                    Message: $"Sent critical error notification to {user.Email}",
//                    TimeStamp: DateTime.Now,
//                    Level: "Information",
//                    Exception: null));

//            }
//            catch (Exception emailEx)
//            {
//                //_logger.LogCritical(emailEx, "Failed to send critical error email");

//                logsCollector.AddLog(GetLog(
//                    UserId: user.Id,
//                    EventType: null,
//                    EventBody: null,
//                    RequestId: context.TraceIdentifier,
//                    RequestPath: context.Request.Path,
//                    EventCriticality: null,
//                    Notes: null,
//                    Message: "Failed to send critical error email",
//                    TimeStamp: DateTime.Now,
//                    Level: "Critical",
//                    Exception: emailEx.Message));

//            }
//        }

//        private async Task HandleNonCriticalError(Exception ex, IEmailSender emailSender, INonCriticalDomainEvent exceptionEvent, CurrentUser user, LogsCollector logsCollector, HttpContext context)
//        {
//            //_logger.LogWarning(ex, "Non-critical event processing failed\nEvent: {EventType}", exceptionEvent.GetType().Name);

//            var evtBody = JsonSerializer.Serialize(exceptionEvent, exceptionEvent.GetType());

//            logsCollector.AddLog(GetLog(
//                UserId: user.Id,
//                EventType: exceptionEvent.GetType().Name,
//                EventBody: evtBody,
//                RequestId: context.TraceIdentifier,
//                RequestPath: context.Request.Path,
//                EventCriticality: "NON-CRITICAL",
//                Notes: null,
//                Message: $"Non-critical event processing failed\nEvent: {exceptionEvent.GetType().Name}",
//                TimeStamp: DateTime.Now,
//                Level: "Warning",
//                Exception: null));


//            try
//            {
//                await emailSender.SendEmailAsync(
//                    "layth.work.syriatel@gmail.com",
//                    CurrentUser.GetDefault().Email,
//                    "Non-Critical System Error",
//                    $"A background operation failed: {ex.Message}");
//            }
//            catch (Exception emailEx)
//            {
//                //_logger.LogError(emailEx, "Failed to send non-critical error email");


//                logsCollector.AddLog(GetLog(
//                    UserId: user.Id,
//                    EventType: null,
//                    EventBody: null,
//                    RequestId: context.TraceIdentifier,
//                    RequestPath: context.Request.Path,
//                    EventCriticality: null,
//                    Notes: null,
//                    Message: "Failed to send non-critical error email",
//                    TimeStamp: DateTime.Now,
//                    Level: "Error",
//                    Exception: emailEx.Message));

//            }
//        }

//        private static CurrentUser GetCurrentUserSafe(HttpContext context, ICurrentUserProvider provider)
//        {
//            try
//            {
//                return context.User?.Identity?.IsAuthenticated == true
//                    ? provider.GetCurrentUser()
//                    : CurrentUser.GetDefault();
//            }
//            catch
//            {
//                return CurrentUser.GetDefault();
//            }
//        }

//        public Log GetLog(
//        int? UserId,
//        string? EventType,
//        string? EventBody,
//        string? RequestId,
//        string? RequestPath,
//        string? EventCriticality,
//        string? Notes,
//        string? Message,
//        DateTime? TimeStamp,
//        string? Level,
//        string? Exception)
//        {
//            return new Log(0, UserId, EventType, EventBody, RequestId, RequestPath, EventCriticality, Notes, Message, TimeStamp, Level, Exception);
//        }
//    }
//}




using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Logging;
using Khadamat_UserManagement.Application.Common.Models;
using Khadamat_UserManagement.Domain.Common.Events;
using Khadamat_UserManagement.Infrastructure.Common.Logging;
using Khadamat_UserManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Khadamat_UserManagement.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EventualConsistencyMiddleware> _logger;

        public EventualConsistencyMiddleware(
            RequestDelegate next,
            ILogger<EventualConsistencyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IPublisher publisher,
            Khadamat_UserDbContext dbContext,
            IEmailSender emailSender,
            ICurrentUserProvider userProvider,
            ILogsCollector logsCollector)
        {
            var currentUser = GetCurrentUserSafe(context, userProvider);
            var notes = currentUser.Id == 0 ?
                "UserId = 0 because this request is being made by an unauthenticated user, like Login or Register, etc... requests" :
                null;

            LogRequestStart(logsCollector, currentUser, notes);

            var transaction = await dbContext.Database.BeginTransactionAsync();

            context.Response.OnCompleted(async () =>
            {
                if(context.Response.StatusCode != 200)
                {
                    return;
                }
                try
                {
                    await ProcessDomainEvents(
                        context, publisher, currentUser, notes,
                        logsCollector, transaction, emailSender);
                }
                catch (Exception ex)
                {
                    await HandleProcessingFailure(
                        ex, emailSender, currentUser,
                        logsCollector, context, transaction);
                }
                finally
                {
                    await FinalizeProcessing(transaction, logsCollector);
                }
            });

            await _next(context);
        }

        private async Task ProcessDomainEvents(
            HttpContext context,
            IPublisher publisher,
            CurrentUser currentUser,
            string? notes,
            ILogsCollector logsCollector,
            IDbContextTransaction transaction,
            IEmailSender emailSender)
        {
            if (context.Items.TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> domainEvents)
            {
                LogDomainEventsFound(logsCollector, currentUser, notes, domainEvents.Count);

                await ProcessCriticalEvents(
                    domainEvents, publisher, currentUser,
                    logsCollector);

                await transaction.CommitAsync();
                LogTransactionCommitted(logsCollector, currentUser);

                await ProcessNonCriticalEvents(
                    domainEvents, publisher, currentUser,
                    logsCollector, emailSender);
            }
            else
            {
                await transaction.CommitAsync();
                LogNoDomainEvents(logsCollector, currentUser, context);
            }
        }

        private async Task ProcessCriticalEvents(
            Queue<IDomainEvent> domainEvents,
            IPublisher publisher,
            CurrentUser currentUser,
            ILogsCollector logsCollector)
        {
            foreach (var evt in domainEvents.OfType<ICriticalDomainEvent>())
            {
                string evtBody = JsonSerializer.Serialize(evt, evt.GetType());
                string evtType = evt.GetType().Name;

                try
                {
                    await publisher.Publish(evt);
                    LogEventProcessed(
                        logsCollector, currentUser,
                        evtType, evtBody, "CRITICAL", "Information",
                        $"Processed critical event {evtType}");
                }
                catch (Exception ex)
                {
                    LogEventFailed(
                        logsCollector, currentUser,
                        evtType, evtBody, "CRITICAL", "Critical",
                        $"CRITICAL FAILURE processing {evtType}", ex);
                    throw;
                }
            }
        }

        private async Task ProcessNonCriticalEvents(
            Queue<IDomainEvent> domainEvents,
            IPublisher publisher,
            CurrentUser currentUser,
            ILogsCollector logsCollector,
            IEmailSender emailSender)
        {
            foreach (var evt in domainEvents.OfType<INonCriticalDomainEvent>())
            {
                string evtBody = JsonSerializer.Serialize(evt, evt.GetType());
                string evtType = evt.GetType().Name;

                try
                {
                    await publisher.Publish(evt);
                    LogEventProcessed(
                        logsCollector, currentUser,
                        evtType, evtBody, "NON-CRITICAL", "Information",
                        $"Processed non-critical event {evtType}");
                }
                catch (Exception ex)
                {
                    LogEventFailed(
                        logsCollector, currentUser,
                        evtType, evtBody, "NON-CRITICAL", "Warning",
                        $"Non-critical event {evtType} failed", ex);
                    await HandleNonCriticalError(
                        ex, emailSender, evt, currentUser,
                        logsCollector);
                }
            }
        }

        private async Task HandleProcessingFailure(
            Exception ex,
            IEmailSender emailSender,
            CurrentUser currentUser,
            ILogsCollector logsCollector,
            HttpContext context,
            IDbContextTransaction transaction)
        {
            LogCatastrophicFailure(logsCollector, currentUser, context, ex);
            await transaction.RollbackAsync();
            await HandleCriticalError(ex, emailSender, currentUser, logsCollector, context);
        }

        private async Task FinalizeProcessing(
            IDbContextTransaction transaction,
            ILogsCollector logsCollector)
        {
            await transaction.DisposeAsync();
            await logsCollector.CommitLogs();
        }

        private void LogRequestStart(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            string? notes)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                Notes: notes,
                Message: $"Starting request processing for user {currentUser.Id}",
                TimeStamp: DateTime.Now,
                Level: "Information"));
        }

        private void LogDomainEventsFound(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            string? notes,
            int count)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                Notes: notes,
                Message: $"Found {count} domain events to process",
                TimeStamp: DateTime.Now,
                Level: "Information"));
        }

        private void LogTransactionCommitted(
            ILogsCollector logsCollector,
            CurrentUser currentUser)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                Message: "Transaction committed",
                TimeStamp: DateTime.Now,
                Level: "Information"));
        }

        private void LogNoDomainEvents(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            HttpContext context)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                EventType: null,
                EventBody: null,
                RequestId: context.TraceIdentifier,
                RequestPath: context.Request.Path,
                EventCriticality: null,
                Notes: null,
                Message: "No domain events to process",
                TimeStamp: DateTime.Now,
                Level: "Information",
                Exception: null));
        }

        private void LogEventProcessed(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            string eventType,
            string eventBody,
            string criticality,
            string level,
            string message)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                EventType: eventType,
                EventBody: eventBody,
                EventCriticality: criticality,
                Message: message,
                TimeStamp: DateTime.Now,
                Level: level));
        }

        private void LogEventFailed(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            string eventType,
            string eventBody,
            string criticality,
            string level,
            string message,
            Exception ex)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                EventType: eventType,
                EventBody: eventBody,
                EventCriticality: criticality,
                Message: message,
                TimeStamp: DateTime.Now,
                Level: level,
                Exception: ex.Message));
        }

        private void LogCatastrophicFailure(
            ILogsCollector logsCollector,
            CurrentUser currentUser,
            HttpContext context,
            Exception ex)
        {
            logsCollector.AddLog(GetLog(
                UserId: currentUser.Id,
                EventType: null,
                EventBody: null,
                RequestId: context.TraceIdentifier,
                RequestPath: context.Request.Path,
                EventCriticality: null,
                Notes: null,
                Message: "CATASTROPHIC FAILURE in event processing - rolled back",
                TimeStamp: DateTime.Now,
                Level: "Critical",
                Exception: ex.Message));
        }

        private async Task HandleCriticalError(
            Exception ex,
            IEmailSender emailSender,
            CurrentUser user,
            ILogsCollector logsCollector,
            HttpContext context)
        {
            logsCollector.AddLog(GetLog(
                UserId: user.Id,
                EventType: null,
                EventBody: null,
                RequestId: context.TraceIdentifier,
                RequestPath: context.Request.Path,
                EventCriticality: null,
                Notes: null,
                Message: "Critical error occurred during event processing",
                TimeStamp: DateTime.Now,
                Level: "Critical",
                Exception: ex.Message));

            try
            {
                await emailSender.SendEmailAsync(
                    "layth.work.syriatel@gmail.com",
                    user.Email,
                    "Critical System Error",
                    $"An important operation failed: {ex.Message}");

                logsCollector.AddLog(GetLog(
                    UserId: user.Id,
                    EventType: null,
                    EventBody: null,
                    RequestId: context.TraceIdentifier,
                    RequestPath: context.Request.Path,
                    EventCriticality: null,
                    Notes: null,
                    Message: $"Sent critical error notification to {user.Email}",
                    TimeStamp: DateTime.Now,
                    Level: "Information",
                    Exception: null));
            }
            catch (Exception emailEx)
            {
                logsCollector.AddLog(GetLog(
                    UserId: user.Id,
                    EventType: null,
                    EventBody: null,
                    RequestId: context.TraceIdentifier,
                    RequestPath: context.Request.Path,
                    EventCriticality: null,
                    Notes: null,
                    Message: "Failed to send critical error email",
                    TimeStamp: DateTime.Now,
                    Level: "Critical",
                    Exception: emailEx.Message));
            }
        }

        private async Task HandleNonCriticalError(
            Exception ex,
            IEmailSender emailSender,
            INonCriticalDomainEvent exceptionEvent,
            CurrentUser user,
            ILogsCollector logsCollector)
        {
            var evtBody = JsonSerializer.Serialize(exceptionEvent, exceptionEvent.GetType());

            logsCollector.AddLog(GetLog(
                UserId: user.Id,
                EventType: exceptionEvent.GetType().Name,
                EventBody: evtBody,
                EventCriticality: "NON-CRITICAL",
                Message: $"Non-critical event processing failed\nEvent: {exceptionEvent.GetType().Name}",
                TimeStamp: DateTime.Now,
                Level: "Warning"));

            try
            {
                await emailSender.SendEmailAsync(
                    "layth.work.syriatel@gmail.com",
                    CurrentUser.GetDefault().Email,
                    "Non-Critical System Error",
                    $"A background operation failed: {ex.Message}");
            }
            catch (Exception emailEx)
            {
                logsCollector.AddLog(GetLog(
                    UserId: user.Id,
                    Message: "Failed to send non-critical error email",
                    TimeStamp: DateTime.Now,
                    Level: "Error",
                    Exception: emailEx.Message));
            }
        }

        private static CurrentUser GetCurrentUserSafe(HttpContext context, ICurrentUserProvider provider)
        {
            try
            {
                return context.User?.Identity?.IsAuthenticated == true
                    ? provider.GetCurrentUser()
                    : CurrentUser.GetDefault();
            }
            catch
            {
                return CurrentUser.GetDefault();
            }
        }

        private Log GetLog(
            int? UserId,
            string? EventType = null,
            string? EventBody = null,
            string? RequestId = null,
            string? RequestPath = null,
            string? EventCriticality = null,
            string? Notes = null,
            string? Message = null,
            DateTime? TimeStamp = null,
            string? Level = null,
            string? Exception = null)
        {
            return new Log(0, UserId, EventType, EventBody, RequestId, RequestPath,
                EventCriticality, Notes, Message, TimeStamp, Level, Exception);
        }
    }
}