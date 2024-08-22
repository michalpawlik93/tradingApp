import { Control } from "react-hook-form";
import { AssetName } from "../../consts/assetName";
import { AssetType } from "../../consts/assetType";
import { Option } from "../presentational/Dropdown";
import { FormDropdown } from "../presentational/FormDropdown";

export interface IAssetFormValues {
  assetName: string;
  assetType: string;
}

interface AssetFormElementsProps<T extends IAssetFormValues> {
  control: Control<T>;
}

export const AssetFormElements = <T extends IAssetFormValues>({
  control,
}: AssetFormElementsProps<T>) => (
  <>
    <FormDropdown
      name="assetName"
      control={control}
      label="Asset Name"
      options={Object.values(AssetName).map((x): Option => [x, x])}
    />
    <FormDropdown
      name="assetType"
      control={control}
      label="Asset Type"
      options={Object.values(AssetType).map((x): Option => [x, x])}
    />
  </>
);
