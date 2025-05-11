import { useTreatments } from "../api/use-treatments";

function TreatmentList() {
  const { data: treatments } = useTreatments();

  return (
    <ul className="grid grid-cols-1 items-stretch gap-4 md:grid-cols-2 lg:grid-cols-3">
      {treatments.map((treatment) => (
        <li
          key={treatment.id}
          className="flex aspect-[1.9] w-full flex-col justify-between rounded-md bg-[#f1f1f1] p-6 lg:aspect-[1.59] xl:aspect-[2] 2xl:aspect-[2.32] dark:bg-[#1b1b1b]"
        >
          <div className="flex flex-col">
            <h1 className="text-base font-medium xl:text-base">
              {treatment.name}
            </h1>
            <p className="text-muted-foreground text-base break-words xl:text-base">
              {treatment.description}
            </p>
          </div>
          <div className="flex items-center justify-between">
            <span className="text-base font-medium xl:text-base">
              ${treatment.price.toFixed(2)}
            </span>
            <span className="text-muted-foreground text-base font-medium xl:text-base">
              {treatment.durationInMinutes} min
            </span>
          </div>
        </li>
      ))}
    </ul>
  );
}

export default TreatmentList;
