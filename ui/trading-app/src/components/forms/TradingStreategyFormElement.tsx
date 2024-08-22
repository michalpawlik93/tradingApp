import { Control } from "react-hook-form";
import { TradingStrategy } from "../../consts/tradingStrategy";
import { Option } from "../presentational/Dropdown";
import { FormDropdown } from "../presentational/FormDropdown";

export interface ITradingStreategyValues {
  tradingStrategy: string;
}

export const tradingStreategyDefaultValues = (): ITradingStreategyValues => ({
  tradingStrategy: TradingStrategy.Scalping,
});

export const TradingStreategyFormElement = <T extends ITradingStreategyValues>({
  control,
}: TradingStreategyFormElementsProps<T>) => (
  <FormDropdown
    name="tradingStrategy"
    control={control}
    label="Trading Strategy"
    options={Object.values(TradingStrategy).map((x): Option => [x, x])}
  />
);

interface TradingStreategyFormElementsProps<T extends ITradingStreategyValues> {
  control: Control<T>;
}
