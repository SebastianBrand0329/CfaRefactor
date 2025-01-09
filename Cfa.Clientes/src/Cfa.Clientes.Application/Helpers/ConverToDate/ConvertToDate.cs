namespace Cfa.Clientes.Application.Helpers.ConverToDate;

public static class ConvertToDate
{
    public static DateTime ConvertToDates(DateTime date)
    {
        string fechaFormateada = date.ToString("dd/MM/yyyy");

        DateTime.TryParseExact(fechaFormateada, "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime fechaNacimiento);

        return fechaNacimiento;
    }
}