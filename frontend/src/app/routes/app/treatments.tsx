import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
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
        <ul className="grid grid-cols-1 items-stretch gap-4 md:grid-cols-2 lg:grid-cols-3">
          {Array(4)
            .fill(-1)
            .map(() => (
              <li className="flex aspect-[2.32] w-full flex-col justify-between gap-14 rounded-md bg-[#f4f4f4] p-6 dark:bg-[#1b1b1b]">
                <div className="flex flex-col gap-0.5">
                  <h1 className="text-base font-medium xl:text-base">
                    Classic Haircut
                  </h1>
                  <p className="text-muted-foreground text-base xl:text-base">
                    A standard haircut service including consultation, wash,
                    cut, and style.
                  </p>
                </div>
                <div className="flex items-center justify-between">
                  <span className="text-base font-medium xl:text-base">
                    $40.00
                  </span>
                  <span className="text-muted-foreground text-base font-medium xl:text-base">
                    30 min
                  </span>
                </div>
              </li>
            ))}
        </ul>
      </div>
    </div>
  );
}
