import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

export const Route = createFileRoute("/app/staff")({
  component: RouteComponent,
});

function RouteComponent() {
  const [open, setOpen] = useState(false);

  return (
    <div className="flex min-h-full w-full flex-col gap-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">Staff</h1>
        <DrawerDialog
          open={open}
          onOpenChange={setOpen}
          trigger={<Button size="sm">Create Staff</Button>}
          title="Create Staff"
          description="
Fill in the details for the new staff."
        >
          TODO
        </DrawerDialog>
      </div>
    </div>
  );
}
