import { fireEvent, render, screen } from "@testing-library/react";
import { useForm } from "react-hook-form";
import { FormInput } from "../FormInput";

interface FormValues {
  [key: string]: string | number;
}

const MockForm = () => {
  const { control, handleSubmit } = useForm<FormValues>({
    defaultValues: {
      exampleText: "",
      exampleNumber: 0,
    },
  });

  const onSubmit = (_data: FormValues) => {};

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <FormInput name="exampleText" control={control} label="Example Text" type="text" />
      <FormInput name="exampleNumber" control={control} label="Example Number" type="number" />
      <button type="submit">Submit</button>
    </form>
  );
};

test("renders FormInput component with text type", () => {
  render(<MockForm />);

  const textInput = screen.getByLabelText(/example text/i) as HTMLInputElement;
  expect(textInput).toBeInTheDocument();
  expect(textInput.type).toBe("text");

  fireEvent.change(textInput, { target: { value: "Test Value" } });
  expect(textInput.value).toBe("Test Value");
});

test("renders FormInput component with number type", () => {
  render(<MockForm />);

  const numberInput = screen.getByLabelText(/example number/i) as HTMLInputElement;
  expect(numberInput).toBeInTheDocument();
  expect(numberInput.type).toBe("number");

  fireEvent.change(numberInput, { target: { value: "123" } });
  expect(numberInput.value).toBe("123");
});
