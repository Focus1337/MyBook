namespace MyBook.FSharpServices.EmailServices

open MimeKit
open MyBook.FSharpServices.EmailServices

type IEmailService =
   abstract member SendEmail : message:Message -> MimeMessage