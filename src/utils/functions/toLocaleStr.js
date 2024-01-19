export default (number) =>{
    const parsed = parseFloat(number);
    if (!isNaN(parsed)) {
        return parsed.toLocaleString('pl-PL', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        })
    }
    return number;
}