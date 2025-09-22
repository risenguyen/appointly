import { ReactNode } from "react";

type ContentLayoutProps = {
  title: string;
  action: ReactNode;
  children: ReactNode;
};

function ContentLayout({ title, action, children }: ContentLayoutProps) {
  return (
    <div className="flex min-h-full w-full flex-1 flex-col gap-8">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">{title}</h1>
        {action}
      </div>

      <div className="flex max-w-full flex-1 flex-col">{children}</div>
    </div>
  );
}

export default ContentLayout;
