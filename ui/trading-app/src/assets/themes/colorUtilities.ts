import { getContrastRatio } from "@mui/material";

export const calculateContrastColor = (
  baseColor: string,
  color: string,
  fallbackColor: string,
): string => {
  const ratio = getContrastRatio(baseColor, color);
  if (ratio > 1.6) {
    return color;
  }
  return fallbackColor;
};
