import { useStaff } from "../api/use-staff";
import type { StaffResponse } from "@/api";

function formatPhoneNumber(phoneNumber: string) {
  const cleaned = phoneNumber.replace(/\D/g, "");
  if (cleaned.length === 10) {
    return cleaned.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
  }
  return phoneNumber;
}

function StaffItem({ staffMember }: { staffMember: StaffResponse }) {
  return (
    <li className="bg-card-2 relative flex aspect-[1.9] w-full flex-col justify-between rounded-md p-6 md:aspect-[1.72] lg:aspect-[1.49] xl:aspect-[2] 2xl:aspect-[2.38]">
      <div className="flex flex-col gap-0.5">
        <h1 className="text-base font-medium">
          {staffMember.firstName} {staffMember.lastName}
        </h1>
        <p className="text-muted-foreground text-base break-words">
          {staffMember.email}
        </p>
      </div>

      <div className="flex items-center justify-between">
        <span className="text-muted-foreground">
          {staffMember.phone ? formatPhoneNumber(staffMember.phone) : null}
        </span>
      </div>
    </li>
  );
}

function StaffList() {
  const { data: staff } = useStaff();

  if (staff.length === 0) {
    return (
      <div className="top-0 flex h-full w-full flex-1 flex-col items-center gap-6 rounded-lg p-8 text-center">
        <div className="flex flex-col items-center gap-2 pt-[26vh]">
          <p className="text-foreground text-xl font-medium">
            No staff members yet.
          </p>
          <p className="text-muted-foreground text-base">
            Get started by creating a new staff member.
          </p>
        </div>
      </div>
    );
  }

  return (
    <ul className="grid grid-cols-1 items-stretch gap-5 md:grid-cols-2 md:gap-4 lg:grid-cols-3">
      {staff.map((staffMember) => (
        <StaffItem key={staffMember.id} staffMember={staffMember} />
      ))}
    </ul>
  );
}

export default StaffList;
