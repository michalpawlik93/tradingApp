import {CombinedQuote} from "../types/CombinedQuote";

export const getAxisYDomain = (
    initialData: CombinedQuote[] | undefined,
    from: string,
    to: string,
    ref: keyof CombinedQuote,
    offset: number
  ) => {
    if (!initialData || initialData.length === 0) {
      return [0, 100];
    }
    const fromDate = new Date(from);
    const toDate = new Date(to);
  
    if (isNaN(fromDate.getTime()) || isNaN(toDate.getTime())) {
      return [0, 100];
    }
    const refData = initialData.filter((d) => {
      const currentDate = new Date(d.ohlc.date);
      return currentDate >= fromDate && currentDate <= toDate;
    });
  
    if (refData.length === 0) {
      return [0, 100];
    }
  
    let [bottom, top] = [refData[0][ref], refData[refData.length-1][ref]];
    refData.forEach((d) => {
      if (d[ref] > top) top = d[ref];
      if (d[ref] < bottom) bottom = d[ref];
    });
  
    return[(bottom as number || 0) - offset, (top as number || 0) + offset];
  };