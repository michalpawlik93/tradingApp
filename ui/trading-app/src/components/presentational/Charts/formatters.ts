export const cypherBFormatter = (params: any): string => {
  const wt1 = params.find((x: any) => x.seriesName === "WaveTrend WT1")?.value[1];
  const wt2 = params.find((x: any) => x.seriesName === "WaveTrend WT2")?.value[1];
  const buy = params.find((x: any) => x.seriesName === "Buy Signal")?.value[1];
  const sell = params.find((x: any) => x.seriesName === "Sell Signal")?.value[1];
  const vwap = params.find((x: any) => x.seriesName === "WaveTrend Vwap")?.value[1];
  const mfiBuy = params.find((x: any) => x.seriesName === "Mfi Buy")?.value[1];
  const mfiSell = params.find((x: any) => x.seriesName === "Mfi Sell")?.value[1];

  return `
    ${buildDateForrmater(params)}
    ${wt1 === undefined ? "" : `<strong>WT1</strong> ${wt1}<br>`}
    ${wt2 === undefined ? "" : `<strong>WT2</strong> ${wt2}<br>`}
    ${vwap === undefined ? "" : `<strong>Vwap</strong> ${vwap}<br>`}
    ${mfiBuy === undefined ? "" : `<strong>Money Flow Index Buy</strong> ${mfiBuy}<br>`}
    ${mfiSell === undefined ? "" : `<strong>Money Flow Index Sell</strong> ${mfiSell}<br>`}
    ${sell === undefined ? "" : `<strong>Sell signal appeared</strong><br>`}
    ${buy === undefined ? "" : `<strong>Buy signal appeared</strong>`}
    ${buildSrsiForrmater(params)}
    ${buildOhlcForrmater(params)}
  `;
};

export const srsiFormatter = (params: any): string => `
    ${buildDateForrmater(params)}
    ${buildSrsiForrmater(params)}
    ${buildOhlcForrmater(params)}
  `;

export const ohlcFormatter = (params: any): string => `
    ${buildDateForrmater(params)}
    ${buildOhlcForrmater(params)}
  `;

const buildDateForrmater = (params: any): string =>
  `<strong>Date:</strong> ${new Date(params[0].value[0]).toLocaleString()}<br>`;

const buildOhlcForrmater = (params: any): string => {
  const open = params.find((x: any) => x.seriesName === "Ohlc")?.value[1];
  const close = params.find((x: any) => x.seriesName === "Ohlc")?.value[2];
  const low = params.find((x: any) => x.seriesName === "Ohlc")?.value[3];
  const high = params.find((x: any) => x.seriesName === "Ohlc")?.value[4];
  return `
    ${open === undefined ? "" : `<strong>Open</strong> ${open}<br>`}
    ${close === undefined ? "" : `<strong>Close</strong> ${close}<br>`}
    ${low === undefined ? "" : `<strong>Low</strong> ${low}<br>`}
    ${high === undefined ? "" : `<strong>High</strong> ${high}<br>`}
  `;
};

const buildSrsiForrmater = (params: any): string => {
  const srsiK = params.find((x: any) => x.seriesName === "Srsi %K")?.value[1];
  const srsiD = params.find((x: any) => x.seriesName === "Srsi %D")?.value[1];
  const srsiBuy = params.find((x: any) => x.seriesName === "Srsi Buy")?.value[1];
  const srsiSell = params.find((x: any) => x.seriesName === "Srsi Sell")?.value[1];

  return `
    ${srsiK === undefined ? "" : `<strong>Srsi %K</strong> ${srsiK}<br>`}
    ${srsiD === undefined ? "" : `<strong>Srsi %D</strong> ${srsiD}<br>`}
    ${srsiSell === undefined ? "" : `<strong>Srsi sell signal appeared</strong><br>`}
    ${srsiBuy === undefined ? "" : `<strong>Srsi buy signal appeared</strong>`}
  `;
};
