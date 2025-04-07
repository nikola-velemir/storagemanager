import React from "react";

interface ComponentCardProps {
  id: string;
  name: string;
  identifier: string;
}

const ComponentCard = ({ id, name, identifier }: ComponentCardProps) => {
  return (
    <div
      id="toast-default"
      className="flex my-4 w-11/12 mx-2 items-center justify-between p-4 text-gray-500 bg-gray-700 rounded-xl shadow-sm dark:text-gray-400 dark:bg-gray-800"
      role="alert"
    >
      <div className="flex flex-row items-center content-center">
        <div className="inline-flex items-center justify-center shrink-0 w-12 h-12 text-gray-800 bg-gray-400 rounded-xl rounded-r-none dark:bg-blue-800 dark:text-blue-200">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            className="size-8"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M10.343 3.94c.09-.542.56-.94 1.11-.94h1.093c.55 0 1.02.398 1.11.94l.149.894c.07.424.384.764.78.93.398.164.855.142 1.205-.108l.737-.527a1.125 1.125 0 0 1 1.45.12l.773.774c.39.389.44 1.002.12 1.45l-.527.737c-.25.35-.272.806-.107 1.204.165.397.505.71.93.78l.893.15c.543.09.94.559.94 1.109v1.094c0 .55-.397 1.02-.94 1.11l-.894.149c-.424.07-.764.383-.929.78-.165.398-.143.854.107 1.204l.527.738c.32.447.269 1.06-.12 1.45l-.774.773a1.125 1.125 0 0 1-1.449.12l-.738-.527c-.35-.25-.806-.272-1.203-.107-.398.165-.71.505-.781.929l-.149.894c-.09.542-.56.94-1.11.94h-1.094c-.55 0-1.019-.398-1.11-.94l-.148-.894c-.071-.424-.384-.764-.781-.93-.398-.164-.854-.142-1.204.108l-.738.527c-.447.32-1.06.269-1.45-.12l-.773-.774a1.125 1.125 0 0 1-.12-1.45l.527-.737c.25-.35.272-.806.108-1.204-.165-.397-.506-.71-.93-.78l-.894-.15c-.542-.09-.94-.56-.94-1.109v-1.094c0-.55.398-1.02.94-1.11l.894-.149c.424-.07.765-.383.93-.78.165-.398.143-.854-.108-1.204l-.526-.738a1.125 1.125 0 0 1 .12-1.45l.773-.773a1.125 1.125 0 0 1 1.45-.12l.737.527c.35.25.807.272 1.204.107.397-.165.71-.505.78-.929l.15-.894Z"
            />
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
            />
          </svg>

          <span className="sr-only">Fire icon</span>
        </div>
        <div className="h-12 px-8 flex flex-col border-l-2 text-sm font-normal text-gray-400">
          Id:
          <span className="ms-2 font-medium text-base text-white">{id}</span>
        </div>
        <div className="h-12 px-8 flex flex-col border-l-2 text-sm font-normal text-gray-400">
          Identifier:
          <span className="ms-2 font-medium text-base text-white">
            {identifier}
          </span>
        </div>
        <div className="h-12 px-8 flex flex-col border-l-2 text-sm font-normal text-gray-400">
          Name:
          <span className="ms-2 font-medium text-base text-white">{name}</span>
        </div>
      </div>
      <button
        type="button"
        className="w-fit my-auto text-white bg-green-700 hover:bg-green-800 focus:outline-none focus:ring-4 focus:ring-green-300 font-medium rounded-xl text-sm px-5 py-2.5 text-center dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800"
      >
        More info
      </button>
    </div>
  );
};

export default ComponentCard;
