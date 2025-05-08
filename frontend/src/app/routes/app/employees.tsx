import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

export const Route = createFileRoute("/app/employees")({
  component: RouteComponent,
});

function RouteComponent() {
  const [open, setOpen] = useState(false);

  return (
    <div className="min-h-full w-full py-2">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">Employees</h1>
        <DrawerDialog
          open={open}
          onOpenChange={setOpen}
          trigger={<Button size="sm">Create Employee</Button>}
          title="Create Employee"
          description="
Fill in the details for the new employee."
        >
          TODO
        </DrawerDialog>
      </div>
    </div>
  );
}
