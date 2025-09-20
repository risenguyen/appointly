import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";
import ContentLayout from "./-layouts/content-layout";
import CreateStaffForm from "@/features/staff/components/create-staff-form";

export const Route = createFileRoute("/app/staff")({
  component: RouteComponent,
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
          trigger={<Button size="sm">Create Staff</Button>}
          title="Create Staff"
          description="Fill in the details for the new staff member."
        >
          <CreateStaffForm setOpen={setCreateStaffOpen} />
        </DrawerDialog>
      }
    >
      {/* No staff members to display yet */}
      <></>
    </ContentLayout>
  );
}
