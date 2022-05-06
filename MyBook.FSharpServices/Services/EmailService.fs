namespace MyBook.FSharpServices.EmailServices

open MailKit.Net.Smtp
open MimeKit
open MyBook.FSharpServices.EmailServices


type EmailService() =
    let CreateEmailMessage (message: Message) =
        let emailMessage = MimeMessage()
        emailMessage.From.Add(MailboxAddress("MyBook", "ЛОГИН ПОЧТЫ"))
        emailMessage.To.Add(MailboxAddress(message.To))
        emailMessage.Subject <- message.Subject
        let body =
            TextPart(MimeKit.Text.TextFormat.Html)
        body.Text <- message.Content
        emailMessage.Body <- body
        emailMessage

    let Send (mailMessage: MimeMessage) =
        use client = new SmtpClient()
        try
            client.Connect("smtp.bk.ru", 465, true)
            let _ =
                client.AuthenticationMechanisms.Remove("XOAUTH2")
            client.Authenticate("ЛОГИН ПОЧТЫ", "ПАРОЛЬ ПОЧТЫ")
            client.Send(mailMessage)
            mailMessage
        finally
            client.Disconnect(true)
            client.Dispose()

    interface IEmailService with
        member _.SendEmail (message: Message) =
            let emailMessage = CreateEmailMessage message
            Send emailMessage
