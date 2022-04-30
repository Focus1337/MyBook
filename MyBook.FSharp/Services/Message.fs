//namespace MyBook.FSharp     
//open MimeKit
namespace MyBook.FSharp.Services.EmailServices

open System.Collections.Generic
open System.Linq
open MimeKit

type Message() =
    member val To = Unchecked.defaultof<List<MailboxAddress>> with get, set
    member val Subject = Unchecked.defaultof<string> with get, set
    member val Content = Unchecked.defaultof<string> with get, set
    
    new(``to`` : IEnumerable<string>, subject : string, content : string) as this =        
        (Message ())
        then
            this.To <- List<MailboxAddress>()
            this.To.AddRange (``to``.Select(fun x -> MailboxAddress(x)))
            this.Subject <- subject
            this.Content <- content