using CertiEx.Common.Enums;

namespace CertiEx.Web.Extensions;

public class AlertExtension
{
    public static string ShowAlert(AlertsEnum obj, string message)
    {            
        string alertDiv = null;
        switch (obj)
        {
            case AlertsEnum.Success:                    
                alertDiv = @"<div class='w3-panel w3-green w3-display-container'><span onclick = this.parentElement.style.display='none' class='w3-button w3-large w3-display-topright'>&times;</span><h3>Success!</h3><p>"+message+"</p></div>";
                break;
            case AlertsEnum.Danger:
                alertDiv = @"<div class='w3-panel w3-red w3-display-container'><span onclick= this.parentElement.style.display='none' class='w3-button w3-large w3-display-topright'>&times;</span><h3>Danger!</h3><p>" + message + "</p></div>";
                break;
            case AlertsEnum.Info:
                alertDiv = "<div class='w3-panel w3-blue w3-display-container'><span onclick = this.parentElement.style.display='none' class='w3-button w3-large w3-display-topright'>&times;</span><h3>Info!</h3><p>"+message+"</p></div>";
                break;
            case AlertsEnum.Warning:
                alertDiv = "<div class='w3-panel w3-yellow w3-display-container'><span onclick = this.parentElement.style.display='none' class='w3-button w3-large w3-display-topright'>&times;</span><h3>Warning!</h3><p>"+message+"</p></div>";
                break;
        }
        return alertDiv;
    }
}