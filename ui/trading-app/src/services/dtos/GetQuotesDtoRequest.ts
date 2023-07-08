export interface GetQuotesDtoRequest {
  granularity: string;
  assetType: string;
  assetName: string;
  startDate?: string;
  endDate?: string;
}
