export interface GetQuotesRequestDto {
  technicalIndicators: string[];
  granularity: string;
  assetType: string;
  assetName: string;
  startDate?: string;
  endDate?: string;
}
