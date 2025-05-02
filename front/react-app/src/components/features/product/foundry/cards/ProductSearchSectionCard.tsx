import { useNavigate } from "react-router-dom";

interface ProductSearchSectionCardProps {
  id: string;
  identifier: string;
  date: string;
  name: string;
  emitProductId: (id: string) => void;
}
const ProductSearchSectionCard = ({
  id,
  identifier,
  date,
  name,
  emitProductId,
}: ProductSearchSectionCardProps) => {
  const handleAddProduct = () => {
    emitProductId(id);
  };
  return (
    <div className="w-11/12 my-4 bg-gray-700 text-white rounded-2xl shadow-md p-4">
      <div className="flex justify-between items-center">
        <div className="flex items-center gap-4">
          <div className="bg-slate-800 rounded-xl p-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              stroke="currentColor"
              className="size-8"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="m21 7.5-9-5.25L3 7.5m18 0-9 5.25m9-5.25v9l-9 5.25M3 7.5l9 5.25M3 7.5v9l9 5.25m0-9v9"
              />
            </svg>
          </div>

          <div className="space-y-1">
            <div className="text-sm text-gray-400">
              <span className="font-light">Identifier:</span>
              <span className="ms-2 font-medium text-base text-white">
                {identifier}
              </span>
            </div>
            <div className="text-sm text-gray-400">
              <span className="font-light">Name:</span>
              <span className="ms-2 font-medium text-base text-white">
                {name}
              </span>
            </div>
            <div className="text-sm text-gray-400">
              <span className="font-light">Date:</span>
              <span className="ms-2 font-medium text-base text-white">
                {date}
              </span>
            </div>
          </div>
        </div>

        <button
          onClick={handleAddProduct}
          className="bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
        >
          Add
        </button>
      </div>
    </div>
  );
};

export default ProductSearchSectionCard;
