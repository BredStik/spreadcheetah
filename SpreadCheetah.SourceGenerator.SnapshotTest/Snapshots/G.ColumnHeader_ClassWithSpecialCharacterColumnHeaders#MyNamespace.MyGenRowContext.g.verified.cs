﻿//HintName: MyNamespace.MyGenRowContext.g.cs
// <auto-generated />
#nullable enable
using SpreadCheetah;
using SpreadCheetah.SourceGeneration;
using SpreadCheetah.Styling;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNamespace
{
    public partial class MyGenRowContext
    {
        private static MyGenRowContext? _default;
        public static MyGenRowContext Default => _default ??= new MyGenRowContext();

        public MyGenRowContext()
        {
        }

        private WorksheetRowTypeInfo<MyNamespace.ClassWithSpecialCharacterColumnHeaders>? _ClassWithSpecialCharacterColumnHeaders;
        public WorksheetRowTypeInfo<MyNamespace.ClassWithSpecialCharacterColumnHeaders> ClassWithSpecialCharacterColumnHeaders => _ClassWithSpecialCharacterColumnHeaders
            ??= WorksheetRowMetadataServices.CreateObjectInfo<MyNamespace.ClassWithSpecialCharacterColumnHeaders>(
                AddHeaderRow0Async, AddAsRowAsync, AddRangeAsRowsAsync, null);

        private static async ValueTask AddHeaderRow0Async(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.Styling.StyleId? styleId, CancellationToken token)
        {
            var headerNames = ArrayPool<string?>.Shared.Rent(8);
            try
            {
                headerNames[0] = "First name";
                headerNames[1] = "";
                headerNames[2] = "Nationality (escaped characters \", ', \\)";
                headerNames[3] = "Address line 1 (escaped characters \n, \t)";
                headerNames[4] = "Address line 2 (verbatim\nstring: \", \\)";
                headerNames[5] = "    Age (\n        raw\n        string\n        literal\n    )";
                headerNames[6] = "Note (unicode escape sequence 🌉, 👍, ç)";
                headerNames[7] = "Note 2 (constant interpolated string: This is a constant)";
                await spreadsheet.AddHeaderRowAsync(headerNames.AsMemory(0, 8), styleId, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<string?>.Shared.Return(headerNames, true);
            }
        }

        private static ValueTask AddAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, MyNamespace.ClassWithSpecialCharacterColumnHeaders? obj, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);
            return AddAsRowInternalAsync(spreadsheet, obj, token);
        }

        private static ValueTask AddRangeAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithSpecialCharacterColumnHeaders?> objs,
            CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (objs is null)
                throw new ArgumentNullException(nameof(objs));
            return AddRangeAsRowsInternalAsync(spreadsheet, objs, token);
        }

        private static async ValueTask AddAsRowInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithSpecialCharacterColumnHeaders obj,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(8);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static async ValueTask AddRangeAsRowsInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithSpecialCharacterColumnHeaders?> objs,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(8);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                foreach (var obj in objs)
                {
                    await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
                }
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static ValueTask AddCellsAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithSpecialCharacterColumnHeaders? obj,
            DataCell[] cells, IReadOnlyList<StyleId> styleIds, CancellationToken token)
        {
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);

            cells[0] = new DataCell(obj.FirstName);
            cells[1] = new DataCell(obj.LastName);
            cells[2] = new DataCell(obj.Nationality);
            cells[3] = new DataCell(obj.AddressLine1);
            cells[4] = new DataCell(obj.AddressLine2);
            cells[5] = new DataCell(obj.Age);
            cells[6] = new DataCell(obj.Note);
            cells[7] = new DataCell(obj.Note2);
            return spreadsheet.AddRowAsync(cells.AsMemory(0, 8), token);
        }

        private static DataCell ConstructTruncatedDataCell(string? value, int truncateLength)
        {
            return value is null || value.Length <= truncateLength
                ? new DataCell(value)
                : new DataCell(value.AsMemory(0, truncateLength));
        }
    }
}
