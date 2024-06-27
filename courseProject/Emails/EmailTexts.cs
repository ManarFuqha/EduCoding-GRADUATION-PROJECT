using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;

namespace courseProject.Emails
{
    public class EmailTexts
    {

        public static string VerificationCode(string UserName, string VerificationCode)
        {
            var body = $@"
                    <html>
                    <body>
                        <div style='text-align: center; font-family: Arial, sans-serif;'>
                        
                            <h2 style='font-size: 24px;'>Dear {UserName},</h2>
                            <p style='font-size: 18px;'>Thank you for joining our site.</p>
                            <p style='font-size: 18px;'>Here is your code to confirm your email:</p>
                            <h1 style='font-size: 36px; color: #3498db;'>{VerificationCode}</h1>
                            <p style='font-size: 18px;'>Best regards,</p>
                            
                        </div>
<div class=""footer""style='text-align: center';>
            <p>&copy; {DateTime.Now.Year} EduCoding Academy. All rights reserved.</p>
        </div>
                    </body>
                    </html>";

            return body;
        }


        public static string RejectInCourse(string UserName, string CourseName , string companyName)
        {
            var body = $@"<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Course Application Status</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 50px auto;
            background: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding-bottom: 20px;
        }}
        .header img {{
            max-width: 150px;
        }}
        .content {{
            font-size: 16px;
            line-height: 1.6;
        }}
        .content h1 {{
            font-size: 24px;
            color: #333333;
        }}
        .content p {{
            margin: 20px 0;
        }}
        .footer {{
            text-align: center;
            padding-top: 20px;
            font-size: 14px;
            color: #888888;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <!-- Optional: Include logo here -->
            <!-- <img src=""path_to_logo.png"" alt=""Company Logo""> -->
        </div>
        <div class=""content"">
            <h1>Course Application Status</h1>
            <p>Dear {UserName},</p>
            <p>Thank you for your interest in the <strong>{CourseName}</strong> course.</p>
            <p>We regret to inform you that your application for the course has not been successful at this time.</p>
            <p>We received a large number of applications and, after careful consideration, we have decided not to proceed with your application.</p>
            <p>We encourage you to apply for other courses in the future and wish you all the best in your educational endeavors.</p>
            <p>Thank you for your understanding.</p>
            <p>Best regards,</p>
           
        </div>
        <div class=""footer"">
            <p>&copy; {DateTime.Now.Year} EduCoding Academy. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

            return body;
        }



        public static string GetAcceptanceEmailHtml(string userName, string courseName, string companyName)
        {
            var body = $@"<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Course Application Accepted</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 50px auto;
            background: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding-bottom: 20px;
        }}
        .header img {{
            max-width: 150px;
        }}
        .content {{
            font-size: 16px;
            line-height: 1.6;
        }}
        .content h1 {{
            font-size: 24px;
            color: #333333;
        }}
        .content p {{
            margin: 20px 0;
        }}
        .footer {{
            text-align: center;
            padding-top: 20px;
            font-size: 14px;
            color: #888888;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <!-- Optional: Include logo here -->
            <!-- <img src=""path_to_logo.png"" alt=""Company Logo""> -->
        </div>
        <div class=""content"">
            <h1>Congratulations, You're In!</h1>
            <p>Dear {userName},</p>
            <p>We are pleased to inform you that you have been accepted into the <strong>{courseName}</strong> course.</p>
            <p>Your application stood out among many, and we are excited to have you join our community of learners. We believe that you will bring valuable perspectives and enthusiasm to our course.</p>
            <p>Please look for further details in upcoming communications on how to prepare and what to expect as you begin this exciting journey with us.</p>
            <p>We look forward to your participation and are excited to see what you will achieve!</p>
            <p>Best regards,</p>
        </div>
        <div class=""footer"">
            <p>&copy; {DateTime.Now.Year} EduCoding. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

            return body;
        }

        public static string ForgetPassword(string UserName, string VerificationCode)
        {
            var body = $@"
        <html>
            <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <table width='100%' cellpadding='0' cellspacing='0' style='max-width: 600px; margin: 0 auto;'>
                    <tr>
                        <td style='background-color: #f5f5f5; padding: 20px; text-align: center;'>
                            <h1 style='color: #333; font-size: 24px; margin: 0;'>Password Reset Code</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #ffffff; padding: 20px;'>
                            <p style='color: #333; font-size: 16px; margin: 0 0 20px;'>Dear {UserName},</p>
                            <p style='color: #333; font-size: 16px; margin: 0 0 20px;'>We received a request to reset your password. Please use the following a code to reset your password:</p>
                            <p style='text-align: center; font-size: 24px; font-weight: bold; margin: 20px 0;'>{VerificationCode}</p>
                            <p style='color: #333; font-size: 16px; margin: 0 0 20px;'>If you did not request a password reset, please ignore this email or contact support if you have questions.</p>
                            <p style='color: #333; font-size: 16px; margin: 0;'>Thank you,</p>
                            <p style='color: #333; font-size: 16px; margin: 0;'>EduCoding Academy Team</p>
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #f5f5f5; padding: 20px; text-align: center; font-size: 12px; color: #666;'>
                            <p style='margin: 0;'>© {DateTime.Now.Year} EduCoding Academy. All rights reserved.</p>
                        </td>
                    </tr>
                </table>
            </body>
        </html>";


            return body;
        }

        public static string SendBookingEmailAsync(string studentName, string lectureTitle, DateTime lectureDate , TimeSpan startTime, TimeSpan endTime, string instructorName)
        {

            var startDateTime = DateTime.Today.Add(startTime);
            var endDateTime = DateTime.Today.Add(endTime);


            var body = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <title>Lecture Booking Confirmation</title>
        </head>
        <body>
           
            <p>Dear {instructorName},</p>
            <p>We are pleased to inform you that a student has booked a lecture with you. Here are the details:</p>
            <ul>
                <li><strong>Student Name:</strong> {studentName}</li>
                <li><strong>Lecture Title:</strong> {lectureTitle}</li>
                <li><strong>Lecture Date:</strong> {lectureDate:MMMM dd, yyyy} from {startDateTime:hh:mm tt} to {endDateTime:hh:mm tt}</li>
            </ul>
            <p>Please prepare accordingly and reach out to the student if necessary.</p>
            <p>Thank you,</p>
            <p>The EduCoding Team</p>
<tr>
                      
                            <p style='margin: 0;'>© {DateTime.Now.Year} EduCoding Academy. All rights reserved.</p>
                        </td>
                    </tr>
        </body>
        </html>";



                  return body;
        }

        }
}
