export interface GetQuotesRequestDto {
  granularity: string;
  assetType: string;
  assetName: string;
  startDate?: string;
  endDate?: string;
}
