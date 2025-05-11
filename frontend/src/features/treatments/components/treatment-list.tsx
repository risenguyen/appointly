import { useTreatments } from "../api/use-treatments";

import { Ellipsis, SquarePen, Trash2 } from "lucide-react";

import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
} from "@/components/ui/dropdown-menu";

function TreatmentList() {
  const { data: treatments } = useTreatments();

  return (
    <ul className="grid grid-cols-1 items-stretch gap-5 md:grid-cols-2 lg:grid-cols-3">
      {treatments.map((treatment) => (
        <li
          key={treatment.id}
          className="bg-card-2 relative flex aspect-[1.9] w-full flex-col justify-between rounded-md p-6 md:aspect-[1.72] lg:aspect-[1.49] xl:aspect-[2] 2xl:aspect-[2.38]"
        >
          <div className="flex flex-col gap-0.5">
            <h1 className="text-base font-medium xl:text-base">
              {treatment.name}
            </h1>
            <p className="text-muted-foreground text-base break-words xl:text-base">
              {treatment.description}
            </p>
          </div>

          <div className="flex items-center justify-between">
            <span className="text-base xl:text-base">
              ${treatment.price.toFixed(2)}
            </span>
            <span className="text-muted-foreground text-base xl:text-base">
              {treatment.durationInMinutes} min
            </span>
          </div>

          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <button
                aria-label="Open Menu"
                className="absolute top-6 right-6 focus:outline-none"
                type="button"
              >
                <Ellipsis className="cursor-pointer" size="16px" />
              </button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem
                variant="default"
                className="flex justify-between"
              >
                <span>Edit</span>
                <SquarePen />
              </DropdownMenuItem>
              <DropdownMenuItem
                className="flex justify-between"
                variant="destructive"
              >
                <span>Delete</span>
                <Trash2 />
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </li>
      ))}
    </ul>
  );
}

export default TreatmentList;
