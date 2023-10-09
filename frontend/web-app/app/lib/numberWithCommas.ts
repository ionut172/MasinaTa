export function numberWithCommas(pretRezervare: number) {
    return pretRezervare.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}