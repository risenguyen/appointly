import { ErrorComponentProps } from "@tanstack/react-router";

type TypedErrorComponentProps<T> = Omit<ErrorComponentProps, "error"> & {
  error: T;
};

export { type TypedErrorComponentProps };
