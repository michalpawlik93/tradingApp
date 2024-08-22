import { Control } from "react-hook-form";
import { sRsiSettingsDefault } from "../../consts/technicalIndicatorsSettings";
import { FormInput } from "../presentational/FormInput";
import { FormToggle } from "../presentational/FormToggle";

export interface ISrsiSettingsFormValues {
  enabled: boolean;
  channelLength: number;
  stochKSmooth: number;
  stochDSmooth: number;
  overbought: number;
  oversold: number;
}

export const srsiSettingsFormDefaultValues = (): ISrsiSettingsFormValues => sRsiSettingsDefault();

interface SrsiSettingsFormElementsProps<T extends ISrsiSettingsFormValues> {
  control: Control<T>;
}

export const SrsiSettingsFormElements = <T extends ISrsiSettingsFormValues>({
  control,
}: SrsiSettingsFormElementsProps<T>) => (
  <>
    <FormToggle name="enabled" control={control} label="Customization Enabled" />
    <FormInput name="channelLength" control={control} label="Channel length" type="number" />
    <FormInput name="stochKSmooth" control={control} label="Stoch K Smooth" type="number" />
    <FormInput name="stochDSmooth" control={control} label="Stoch D Smooth" type="number" />
    <FormInput name="overbought" control={control} label="Overbought" type="number" />
    <FormInput name="oversold" control={control} label="Oversold" type="number" />
  </>
);
