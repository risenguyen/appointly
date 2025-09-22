import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { staffQueryOptions } from "@/features/staff/api/use-staff";

import { Skeleton } from "@/components/ui/skeleton";
import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

import ContentLayout from "./-layouts/content-layout";
import CreateStaffForm from "@/features/staff/components/create-staff-form";
import StaffList from "@/features/staff/components/staff-list";

export const Route = createFileRoute("/app/staff")({
  component: RouteComponent,
  loader: async ({ context: { queryClient } }) => {
    await queryClient.ensureQueryData(staffQueryOptions());
  },
  pendingComponent: () => {
    return (
      <div className="flex max-h-[calc(100dvh-140px)] w-full flex-1 flex-col gap-8 overflow-hidden">
        <div className="flex items-center justify-between">
          <Skeleton className="h-8 w-36" />
          <Skeleton className="h-8 w-36" />
        </div>

        <div className="max-w-full flex-1">
          <ul className="grid grid-cols-1 items-stretch gap-5 md:grid-cols-2 lg:grid-cols-3">
            {Array.from({
              length: 12,
            }).map((_, index) => (
              <li
                key={index}
                className="bg-card-2 flex aspect-[1.9] w-full flex-col justify-between rounded-md p-6 md:aspect-[1.72] lg:aspect-[1.49] xl:aspect-[2] 2xl:aspect-[2.38]"
              >
                <div className="flex flex-col gap-1">
                  <Skeleton className="h-5 w-3/4" />
                  <Skeleton className="h-4 w-full" />
                  <Skeleton className="h-4 w-5/6" />
                </div>
                <div className="flex items-center justify-between">
                  <Skeleton className="h-5 w-1/4" />
                  <Skeleton className="h-5 w-1/4" />
                </div>
              </li>
            ))}
          </ul>
        </div>
      </div>
    );
  },
});

function RouteComponent() {
  const [createStaffOpen, setCreateStaffOpen] = useState(false);

  return (
    <ContentLayout
      title="Staff"
      action={
        <DrawerDialog
          open={createStaffOpen}
          onOpenChange={setCreateStaffOpen}
          trigger={<Button size="sm">Create Staff Member</Button>}
          title="Create Staff"
          description="Fill in the details for the new staff member."
        >
          <CreateStaffForm setOpen={setCreateStaffOpen} />
        </DrawerDialog>
      }
    >
      <StaffList />
    </ContentLayout>
  );
}
