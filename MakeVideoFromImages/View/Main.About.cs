using MakeVideoFromImages.Metadata;

namespace MakeVideoFromImages;

public partial class Main
{
    private void AboutButton_Click(object? sender, EventArgs e)
    {
        var message =
            $"{ApplicationOwnership.ProductName}{Environment.NewLine}" +
            $"{ApplicationOwnership.CompanyLegalName}{Environment.NewLine}" +
            $"{ApplicationOwnership.CompanyDescription}{Environment.NewLine}" +
            $"Site: {ApplicationOwnership.CompanyUrl}{Environment.NewLine}" +
            $"Responsavel: {ApplicationOwnership.OwnerName}{Environment.NewLine}" +
            $"Email: {ApplicationOwnership.ContactEmail}{Environment.NewLine}" +
            $"Portfolio: {ApplicationOwnership.PortfolioUrl}{Environment.NewLine}" +
            $"GitHub: {ApplicationOwnership.GitHubUrl}{Environment.NewLine}" +
            $"LinkedIn empresa: {ApplicationOwnership.CompanyLinkedInUrl}{Environment.NewLine}" +
            $"LinkedIn: {ApplicationOwnership.LinkedInUrl}";

        MessageBox.Show(this, message, "Sobre", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
