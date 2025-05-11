import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { treatmentsQueryOptions } from "@/features/treatments/api/use-treatments";

import { Skeleton } from "@/components/ui/skeleton";
import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";
import TreatmentList from "@/features/treatments/components/treatment-list";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
  loader: async ({ context: { queryClient } }) => {
    await queryClient.ensureQueryData(treatmentsQueryOptions());
  },
  pendingComponent: () => {
    return (
      <div className="flex min-h-full w-full flex-col gap-6">
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
  const [open, setOpen] = useState(false);

  return (
    <div className="flex min-h-full w-full flex-col gap-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">Treatments</h1>
        <DrawerDialog
          open={open}
          onOpenChange={setOpen}
          trigger={<Button size="sm">Create Treatment</Button>}
          title="Create Treatment"
          description="
Fill in the details for the new treatment."
        >
          <CreateTreatmentForm setOpen={setOpen} />
        </DrawerDialog>
      </div>

      <div className="max-w-full flex-1">
        <TreatmentList />
      </div>
    </div>
  );
}
