namespace MyBook.FSharpServices.EmailServices
type Message() =
    member val To = Unchecked.defaultof<string> with get, set
    member val Subject = Unchecked.defaultof<string> with get, set
    member val Content = Unchecked.defaultof<string> with get, set
    
    new(sendTo : string, subject : string, content : string) as this =        
        (Message ())
        then
            this.To <- sendTo
            this.Subject <- subject
            this.Content <- content