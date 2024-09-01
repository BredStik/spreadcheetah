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

        private WorksheetRowTypeInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties>? _ClassWithColumnOrderForSomeProperties;
        public WorksheetRowTypeInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties> ClassWithColumnOrderForSomeProperties => _ClassWithColumnOrderForSomeProperties
            ??= WorksheetRowMetadataServices.CreateObjectInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties>(
                AddHeaderRow0Async, AddAsRowAsync, AddRangeAsRowsAsync, null);

        private static async ValueTask AddHeaderRow0Async(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.Styling.StyleId? styleId, CancellationToken token)
        {
            var cells = ArrayPool<StyledCell>.Shared.Rent(6);
            try
            {
                cells[0] = new StyledCell("MiddleName", styleId);
                cells[1] = new StyledCell("FirstName", styleId);
                cells[2] = new StyledCell("Score", styleId);
                cells[3] = new StyledCell("LastName", styleId);
                cells[4] = new StyledCell("Employed", styleId);
                cells[5] = new StyledCell("Age", styleId);
                await spreadsheet.AddRowAsync(cells.AsMemory(0, 6), token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<StyledCell>.Shared.Return(cells, true);
            }
        }

        private static ValueTask AddAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties? obj, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);
            return AddAsRowInternalAsync(spreadsheet, obj, token);
        }

        private static ValueTask AddRangeAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties?> objs,
            CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (objs is null)
                throw new ArgumentNullException(nameof(objs));
            return AddRangeAsRowsInternalAsync(spreadsheet, objs, token);
        }

        private static async ValueTask AddAsRowInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties obj,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(6);
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
            IEnumerable<SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties?> objs,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(6);
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
            SpreadCheetah.SourceGenerator.SnapshotTest.Models.ColumnOrdering.ClassWithColumnOrderForSomeProperties? obj,
            DataCell[] cells, IReadOnlyList<StyleId> styleIds, CancellationToken token)
        {
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);

            cells[0] = new DataCell(obj.MiddleName);
            cells[1] = new DataCell(obj.FirstName);
            cells[2] = new DataCell(obj.Score);
            cells[3] = new DataCell(obj.LastName);
            cells[4] = new DataCell(obj.Employed);
            cells[5] = new DataCell(obj.Age);
            return spreadsheet.AddRowAsync(cells.AsMemory(0, 6), token);
        }

        private static DataCell ConstructTruncatedDataCell(string? value, int truncateLength)
        {
            return value is null || value.Length <= truncateLength
                ? new DataCell(value)
                : new DataCell(value.AsMemory(0, truncateLength));
        }
    }
}
